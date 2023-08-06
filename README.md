для настоящих любителей жесткого хардкора я представляю вам сговнокоденную, русскую, обновленную версию 5.0.5 DiscordIntegration

# DiscordIntegration

Бот и плагин, который позволяет серверу логировать действия в дискорд каналы, и посылать серверные команды через дискорд бота

## Минимальные требования
[Node.js](https://nodejs.org/en/) **14.16.0+**

[EXILED](https://github.com/Exiled-Team/EXILED/releases/latest) **6.0.0+**

## Установка
1. Разархивируйте `DiscordIntegration.dll` и его `Plugin.tar.gz`, потом разархивируйте `DiscordIntegration.Bot.tar.gz` в любую папку, которую пожалаете.
2. Скопируйте и вставьте `DiscordIntegration.dll` в папку `Plugins`, и положите зависимости в папку `Plugins/dependencies`.

## Как установить Node.js и npm

### Windows
1. Скачайте Node.js с https://nodejs.org/en/.
2. Откройте файл, и собственно установите

### Linux
1. Нажмите [суда](https://nodejs.org/ru/download/package-manager).
2. Нажмите на ваш дистибутив
3. Следуйте инструкциям

## Как установить библиотеки для бота

### Windows && Linux

1. Откройте командную строку
2. Запустите команду `cd path\to\bot` меняя `path\to\bot` на путь, где находится бот, проверьте, что `index.js` и `package.json` в одинаковых папках.
3. Запустите команду `npm install`.

## Как создать дискорд бота
Зайдите https://discord.com/developers/applications и создайте нового бота и получите его токен (Bot -> Reset token).

## Как запустить бота

Запустите бота чтобы он создал config.yml и synced-roles.yml.

### Windows

1. Откройте командную строку
2. Запустите команду `cd path\to\bot` меняя `path\to\bot` на путь, где находится бот, проверьте, что `index.js` и `package.json` в одинаковых папках.
2. Запустите команду `node index.js`.

### Linux

1. Запустите команду `cd path\to\bot` меняя `path\to\bot` на путь, где находится бот, проверьте, что `index.js` и `package.json` в одинаковых папках.
2. Запустите команду `node index.js`.

## Как использовать команды через дискорд бота

1. Откройте файл `config.yml`.
2. Добавьте в `command` айди канала, откуда нужно использовать команды.

```yaml
channels:
  command:
    - "channel-id-1"
    - "channel-id-2"
    - "channel-id-3"
```

3. Добавьте в список какие именно команды, и какая роль может использовать их.. `.*` чтобы разрешить использование всех команд НА СВОЙ СТРАХ И РИСК!!!

```yaml
commands:
  "role-1":
    - "di"
    - "discordintegration"
  "role-2":
    - ".*"
 ```
## Доступные команды (сорри мне лень переводить, кто-нибудь пуллреквестните мне перевод)

| Команда | Описание | Аргументы | Права | Пример |
| --- | --- | --- | --- | --- |
| di add role | Adds a role-group pair to the SyncedRole list. | **[RoleID] [Group name]** | di.add.role | **di add role 656673336402640902 helper** |
| di add user | Adds an userID-discordID pair to the SyncedRole list. | **[UserID] [DiscordID]** | di.add.user | **di add user 76561198023272004@steam 219862538844635136** |
| di remove role | Removes a role-group pair from the SyncedRole list. | **[RoleID]** | di.remove.role | **di remove role 656673336402640902** |
| di remove user | Removes an userID-discordID pair from the SyncedRole list. | **[UserID]** | di.remove.user | **di remove user 76561198023272004@steam** |
| di reload | Перезагрузить все. | | di.reload | **di reload** |
| di reload configs | Перезагрузить конфиг бота | | di.reload.configs | **di reload configs** |
| di reload language | Перезагрузить язык бота. | | di.reload.language | **di reload language** |
| di reload syncedroles | Перезагрузить syncedroles если подключен. | | di.reload.syncedroles | **di reload syncedroles** |
| di playerlist | Получить список игроков на сервере. | | di.playerlist | **di playerlist** |
| di stafflist | Получить список админов на сервере. | | di.stafflist | **di stafflist** |
