console.clear();

const discord = require('discord.js');
const fs = require('fs');
const yaml = require('js-yaml');
const camelCaseKeys = require('camelcase-keys');
const snakeCaseKeys = require('snakecase-keys');
const sleep = require('util').promisify(setTimeout);

const configPath = './Configs/config.yml';
const syncedRolesPath = './Configs/synced-roles.yml';
const discordClient = new discord.Client();
const tcpServer = require('net').createServer();

let discordServer = null;
let config = {
    token: '',
    prefix: '+',
    channels: {
        log: {
            commands: [
                'channel-id-1'
            ],
            gameEvents: [
                'channel-id-2'
            ],
            bans: [
                'channel-id-3'
            ],
            reports: [
                'channel-id-4'
            ],
        },
        command: [
            'channel-id-6'
        ],
    },
    commands: {
        'role-id-1': ['di', 'discordintegration']
    },
    discordServer: {
        id: ''
    },
    tcpServer: {
        port: 9000,
        ipAddress: '127.0.0.1'
    },
    keepAliveInterval: 2000,
    messagesDelay: 1000,
    isDebugEnabled: false
};
let syncedRoles = {
    roleToGroup: {},
    userIdToDiscordId: {}
};
let messagesQueue = {};
let sockets = [];
let remoteCommands = {
    "loadConfigs": loadConfigs,
    "loadSyncedRoles": loadSyncedRoles,
    "saveSyncedRoles": saveSyncedRoles,
    "getGroupFromId": getGroupFromId,
    "queueMessage": queueMessage,
    "sendMessage": sendMessage,
    "log": log,
    "updateActivity": updateActivity,
    "addUser": addUser,
    "removeUser": removeUser,
    "addRole": addRole,
    "removeRole": removeRole
};

discordClient.on('ready', async () => {

    await discordClient.user.setActivity('за подключениями..', { type: "WATCHING" });
    await discordClient.user.setStatus("idle");

    console.log(`[ДИСКОРД-ИНФОРМАЦИЯ] Успешный вход за ${discordClient.user.tag}.`);
    console.log(`[NET-ИНФОРМАЦИЯ] Включение сервера на ${config.tcpServer.ipAddress}:${config.tcpServer.port}...`);

    tcpServer.listen(config.tcpServer.port, config.tcpServer.ipAddress);
    tcpServer.ref();

    discordClient.guilds.fetch(config.discordServer.id)
        .then(result => discordServer = result)
        .catch(error => {
            console.error(`[ДИСКОРД-ОШИБКА] Инвалидный айди сервера: ${error.message}`);
            process.exit(0);
        });

    await handleMessagesQueue();
    await refreshDiscordServerCache();
});
async function refreshDiscordServerCache() {
    for (; ;) {

        discordClient.guilds.fetch(config.discordServer.id)
            .then(result => discordServer = result)
            .catch(error => {
                console.error(`[ДИСКОРД-ОШИБКА] Инвалидный айди сервера: ${error.message}`);
                process.exit(0);
            });

        await sleep(120000);
    }
}

discordClient.on('message', message => {
    if (!config.channels.command || message.author.bot || !message.content.startsWith(config.prefix) || !config.channels.command.includes(message.channel.id))
        return;

    if (sockets.length === 0) {
        message.channel.send('Сервер не подключён.');
        return;
    }

    const command = message.content.substring(config.prefix.length, message.content.length);

    if (command.length === 0) {
        message.channel.send('Команда не может быть пустой.');
        return;
    }

    if (!canExecuteCommand(message.member, command.toLowerCase())) {
        message.channel.send("Отказано в доступе.");
        return;
    }

    if (config.isDebugEnabled)
        console.debug(`[ДИСКОРД-ДЕБАГ] ${message.author.tag} (${message.author.id}) выполнил команду: [${command}]`);
    sockets.forEach(socket => socket.write(JSON.stringify({ action: 'executeCommand', parameters: { channelId: message.channel.id, content: command, user: { id: message.author.id + '@discord', name: message.author.tag } } }) + '\0'));
});

discordClient.on('warn', warn => console.warn(`[ДИСКОРД-ПРЕДУПРЕЖДЕНИЕ] ${warn}`));

discordClient.on('error', error => console.error(`[ДИСКОРД-ОШИБКА] ${error}`));

tcpServer.on('listening', () => console.log(`[NET-ИНФОРМАЦИЯ] Сервер успешно запустился на ${config.tcpServer.ipAddress}:${config.tcpServer.port}.`));

tcpServer.on('error', error =>
{
    console.error(`[NET-ОШИБКА] ${error === 'EADDRINUSE' ? `${config.address}:${config.port} is already in use!` : `${error}`}`);
    process.exit(0);
});

tcpServer.on('connection', socket =>
{
  socket.setEncoding('UTF-8');
  socket.setKeepAlive(true, config.keepAliveDuration);

  sockets.push(socket);

  console.log(`[NET-ИНФОРМАЦИЯ] Подключение налажено с ${socket.address().address}:${socket.address().port}.`);

    socket.on('data', (buffer) =>
    {
    buffer.split('\0').forEach(async remoteCommand => {
      if (!remoteCommand)
        return;

      try {
        remoteCommand = JSON.parse(remoteCommand);

        if (typeof remoteCommand.action !== 'undefined' && remoteCommand.action in remoteCommands) {


          const returnedValue = await remoteCommands[remoteCommand.action](...remoteCommand.parameters);

          if (returnedValue)
            socket.write(returnedValue + '\0');
        }

      } catch (exception) {
        console.error(`[NET-ОШИБКА] An error has occurred while receiving data from ${socket?.address()?.address}:${socket?.address()?.port}: ${exception}`);
      }
    });
  });

    socket.on('error', error =>
    {
    if (error.message.includes('ECONNRESET')) {
      console.info('[SOCKET-ИНФОРМАЦИЯ] Server closed connection.');
      log('gameEvents', '```diff\n- Server closed connection.\n```', true);
    } else {
      console.error(`[SOCKET-ОШИБКА] Server closed connection: ${error}.`);
      log('gameEvents', `\`\`\`diff\n - Server closed connection: ${error}.\n\`\`\``, true);
    }
  });

  socket.on('close', () => sockets.splice(sockets.indexOf(socket), 1));
});

function canExecuteCommand(member, command)
{
    if (!config.commands || !member || typeof command !== 'string')
        return false;

    for (const roleId in config.commands)
    {
        const exists = config.commands[roleId].some(configCommand => command.startsWith(configCommand.toLowerCase()) || (configCommand === '.*' && member.roles.cache.has(roleId)));

        if (exists) {
            const role = discordServer.roles.cache.get(roleId)

            if (role && member.roles.highest.position >= role.position)
                return true;
        }
    }

    return false;
};

async function loadConfigs()
{
    console.log('[БОТ-ИНФОРМАЦИЯ] Загрузка конфига...');
    try {
        if (!fs.existsSync(configPath)) {
            console.error('[БОТ-ОШИБКА] Файл конфига не был найден, генерация...');

            fs.writeFileSync(configPath, yaml.dump(snakeCaseKeys(config)));
            console.error('[БОТ-ИНФОРМАЦИЯ] Конфиг сгенерирован! Ожидайте...');

            await sleep(1500);
        }

        config = camelCaseKeys(yaml.load(fs.readFileSync(configPath)), { deep: true });

        if (!config) {
            console.error('[БОТ-ОШИБКА] Файл конфига пустой! Закругляемся...');
            process.exit(0);
        }
    } catch (exception) {
        console.error(`[БОТ-ОШИБКА] Error while loading configs: ${exception}`);
        process.exit(0);
    }

    console.log('[БОТ-ИНФОРМАЦИЯ] Конфиги успешно загружены.');
}

function loadSyncedRoles() {
    console.log('[БОТ-ИНФОРМАЦИЯ] Загрузка сихронизированных ролей...');

    try {
        if (!fs.existsSync(syncedRolesPath)) {
            console.error('[БОТ-ОШИБКА] Файл сихронизированных ролей не был найден, генерация...');

            fs.writeFileSync(syncedRolesPath, yaml.dump(snakeCaseKeys(syncedRoles)));
        }

        const tempSyncedRoles = camelCaseKeys(yaml.load(fs.readFileSync(syncedRolesPath)), { deep: true });

        if (tempSyncedRoles)
            syncedRoles = tempSyncedRoles;

    } catch (exception) {
        console.error(`[БОТ-ОШИБКА] Error while loading synced roles: ${exception}`);
        return;
    }

    console.log('[БОТ-ИНФОРМАЦИЯ] Сихронизированные роли успешно загружены.');
}

function saveSyncedRoles() {
    console.log('[БОТ-ИНФОРМАЦИЯ] Сохранение сихронизированных ролей...');

    try {
        fs.writeFileSync(syncedRolesPath, yaml.dump(snakeCaseKeys(syncedRoles, { deep: false })));
    } catch (exception) {
        console.error(`[БОТ-ОШИБКА] Ошибка при сохранении сихронизированных ролей: ${exception}`);
    }

    console.log('[БОТ-ИНФОРМАЦИЯ] Сихронизированных роли были успешно сохранены.');
}

async function getGroupFromId(id) {
    let obtainedId = id.substring(0, id.lastIndexOf('@'));

    if (obtainedId.length === 17) {
        if (id in syncedRoles.userIdToDiscordId)
            obtainedId = syncedRoles.userIdToDiscordId[id];
        else
            return;
    }

    let user;

    try {
        user = await discordServer.members.fetch(obtainedId);
    } catch (exception) {
        console.error(`[БОТ-ОШИБКА] Cannot sync ${id} (${obtainedId}) user ID, user not found! ${exception}`);
        return;
    }

    let group;

    for (const role of user.roles.cache.array()) {
        group = syncedRoles.roleToGroup[role.id];

        if (group)
            break;
    }

    if (!group) {
        console.error(`[БОТ-ОШИБКА] Cannot find group of ${id} (${obtainedId}) in synced roles.`);
        return;
    }

    return JSON.stringify({ action: 'setGroupFromId', parameters: { id: id, group: group } });
}

function queueMessage(channelId, content, shouldLogTimestamp = true) {
    if (shouldLogTimestamp)
        content = `[${(new Date()).toLocaleTimeString()} - ${(new Date()).toLocaleDateString()}] ${content}`;

    if (channelId in messagesQueue)
        messagesQueue[channelId] += '\n' + content;
    else
        messagesQueue[channelId] = content;
}

function sendMessage(channelId, content, shouldLogTimestamp = false) {


    if (shouldLogTimestamp)
        content = `[${(new Date()).toLocaleTimeString()}] ${content}`;

    const channel = discordServer.channels.cache.find(channel => channel.id === channelId);

    channel?.send(content, { split: true })
        .then(result => {

            if (config.isDebugEnabled)
                console.debug(`[ДИСКОРД-ДЕБАГ] "${result}" message has been sent in "${channel.name}" (${channel.id}).`);
        })
        .catch(error => console.error(`[ДИСКОРД-ОШИБКА] Cannot send message in "${channel.name}" (${channel.id}): ${error}`));
}

function log(type, content, isInstant = false)
{
    if (!config.channels.log[type])
        return;

    let stripped = content.replace(/\b((?:[a-z-\w-]+:(?:\/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.-a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?«»“”‘’]))/g, '')
    config.channels.log[type].forEach(channelId => isInstant ? sendMessage(channelId, stripped, true) : queueMessage(channelId, stripped));
}

function updateActivity(newActivity) {
    discordClient.user.setActivity(`за ${newActivity} игроками..`, { type: "WATCHING" })
        .then(async presence => {
            if (config.isDebugEnabled)
                console.debug(`[ДИСКОРД-ДЕБАГ] Bot activity has been set to "${presence.activities}".`);

            await discordClient.user.setStatus(newActivity.startsWith('0') ? 'idle' : 'online');
        })
        .catch(error => console.error(`[ДИСКОРД-ОШИБКА] Cannot set bot activity: ${error}`));
}

function addUser(userId, discordId, sender) {
    syncedRoles.userIdToDiscordId[userId] = discordId;

    saveSyncedRoles();

    return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${userId}: ${discordId} user ID - Discord ID pair has been added to synced roles.`, isSucceeded: true } });
}

function removeUser(userId, sender) {
    if (!syncedRoles || !(userId in syncedRoles.userIdToDiscordId))
        return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${userId} user ID wasn't present in synced roles!`, isSucceeded: false } });

    delete syncedRoles.userIdToDiscordId[userId];

    saveSyncedRoles();

    return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${userId} user ID has been removed from synced roles.`, isSucceeded: true } });
}

function addRole(roleId, group, sender) {
    if (!discordServer.roles.cache.has(roleId)) {
        return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${roleId} role ID wasn't found!`, isSucceeded: false } });
    }

    syncedRoles.roleToGroup[roleId] = group;

    saveSyncedRoles();

    return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${roleId}: ${group} role ID - group pair has been added to synced roles.`, isSucceeded: true } });
}

function removeRole(roleId, sender) {
    if (!syncedRoles || !(roleId in syncedRoles.roleToGroup))
        return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${roleId} role ID wasn't present in synced roles!`, isSucceeded: false } });

    delete syncedRoles.roleToGroup[roleId];

    saveSyncedRoles();

    return JSON.stringify({ action: 'commandReply', parameters: { sender, response: `${roleId} role ID has been removed from synced roles.`, isSucceeded: true } });
}

async function close(exit = false) {
    await discordClient.user.setStatus('invisible');
    await discordClient.user.setActivity('');

    log('gameEvents', '```diff\n- Бот отключён.\n```', true);

    sockets.forEach(socket => socket.destroy());

    sockets = [];

    tcpServer.close(() => {
        console.info('[NET-ИНФОРМАЦИЯ] Сервер закрыт.');
        tcpServer.unref();
    });

    await sleep(500);

    if (exit)
        process.exit(0);
};

async function handleMessagesQueue() {
    for (; ;) {
        for (const channelId in messagesQueue)
            sendMessage(channelId, messagesQueue[channelId]);

        messagesQueue = {};
        await sleep(config.messagesDelay);
    }
}

loadConfigs();
loadSyncedRoles();

console.log('[ДИСКОРД-ИНФОРМАЦИЯ] Подключение...');

discordClient.login(config.token)
  .catch(error => {
    console.error(`[ДИСКОРД-ОШИБКА] Login has failed: ${error}`);
    process.exit(0);
  });

['SIGINT', 'SIGTERM', 'SIGHUP', 'SIGUSR1', 'SIGUSR2'].forEach(event => {
    process.on(event, async () => await close(true));
});

process.on('exit', async () => await close());