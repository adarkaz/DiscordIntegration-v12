// -----------------------------------------------------------------------
namespace DiscordIntegration;
using System;
using System.IO;
using Exiled.API.Features;
using Newtonsoft.Json;
using static DiscordIntegration;

[JsonObject(ItemRequired = Required.Always)]
public sealed class Language
{
    private readonly JsonSerializer jsonSerializer = new JsonSerializer();
    public Language()
    {
        jsonSerializer.Error += Error;
        jsonSerializer.Formatting = Formatting.Indented;
    }
    public static string Folder { get; } = Path.Combine(Paths.Configs, Instance.Name, "Languages");
    public static string FullPath => Path.Combine(Folder, $"{Instance.Config.Language}.json");
    public string UsedCommand { get; set; } = ":keyboard: {0} ({1}) [{2}] использовал команду: {3} {4}";
    public string RestartingServer { get; set; } = ":wastebasket: Перезагрузка сервера..";
    public string HasRunClientConsoleCommand { get; set; } = ":keyboard: {0} ({1}) [{2}] использовал клиенсткую-команду: {3} {4}";
    public string NoPlayersOnline { get; set; } = "Нет игроков онлайн.";
    public string NoStaffOnline { get; set; } = "Нет админов онлайн..";
    public string WaitingForPlayers { get; set; } = $":hourglass: Ожидание игроков..";
    public string RoundStarting { get; set; } = ":arrow_forward: Раунд начинается: {0} игроков в раунде.";
    public string RoundEnded { get; set; } = ":stop_button: Раунд завершен | Победили: {0} | Игроков онлайн: {1}/{2}.";
    public string ReportFilled { get; set; } = ":incoming_envelope: Отправлен репорт:\n{0} ({1}) [{2}] отправил репорт на: {3} ({4}) [{5}] за {6}.";

    public string CheaterReportFilled { get; set; } = ":incoming_envelope: Отправлен репорт на читера:\n{0} ({1}) [{2}] отправил репорт на: {3} ({4}) [{5}] за {6}.";

    public string HasDamagedForWith { get; set; } = ":crossed_swords: **{0} ({1}) [{2}] нанес урон {3} ({4}) [{5}] на {6} с помощью {7}.**";

    public string HasKilledWith { get; set; } = ":skull_crossbones: **{0} ({1}) [{2}] убил {3} ({4}) [{5}] с помощью {6}.**";

    public string ThrewAGrenade { get; set; } = ":boom: {0} ({1}) [{2}] бросил {3}.";

    public string UsedMedicalItem { get; set; } = ":medical_symbol: {0} ({1}) [{2}] использовал {3}.";

    public string ChangedRole { get; set; } = ":mens: {0} ({1}) [{2}] был переведен в {3}.";

    public string ChaosInsurgencyHaveSpawned { get; set; } = ":spy: ПХ заспавнился с {0} игроками.";

    public string NineTailedFoxHaveSpawned { get; set; } = ":cop: МОГ заспавнился с {0} игроками.";

    public string HasJoinedTheGame { get; set; } = ":arrow_right: **{0} ({1}) [{2}] зашёл в игру.**";

    public string HasBeenFreedBy { get; set; } = ":unlock: {0} ({1}) [{2}] был освобожден {3} ({4}) [{5}].";

    public string HasBeenHandcuffedBy { get; set; } = ":lock: {0} ({1}) [{2}] был связан {3} ({4}) [{5}].";

    public string WasKicked { get; set; } = ":warning: {0} ({1}) был кикнут {2}.";

    public string WasBannedBy { get; set; } = ":no_entry: {0} ({1}) был забанен {2} за {3} до {4}.";

    public string HasStartedUsingTheIntercom { get; set; } = ":loud_sound: {0} ({1}) [{2}] начал использовать интерком.";

    public string HasPickedUp { get; set; } = "{0} ({1}) [{2}] поднял **{3}**.";

    public string HasDropped { get; set; } = "{0} ({1}) [{2}] выкинул **{3}**.";

    public string DecontaminationHasBegun { get; set; } = ":biohazard: **Обеззараживание началось.**";

    public string HasEnteredPocketDimension { get; set; } = ":door: {0} ({1}) [{2}] попал в измерение 106.";

    public string HasEscapedPocketDimension { get; set; } = ":high_brightness: {0} ({1}) [{2}] вышёл из измерения 106.";

    public string PlayerTriggeredTeslaGate { get; set; } = ":zap: {0} ({1}) [{2}] активировал тесла-ворота на протяжении {3}";
    public string SCP079ActivatedTeslaGate { get; set; } = ":zap: {0} ({1}) [{2}] использовал способность активации тесла-ворот.";

    public string Scp914ProcessedItem { get; set; } = ":gear: SCP-914 обработал: **{0}**";

    public string HasClosedADoor { get; set; } = ":door: {0} ({1}) [{2}] закрыл {3} дверь.";

    public string HasOpenedADoor { get; set; } = ":door: {0} ({1}) [{2}] открыл {3} дверь.";

    public string Scp914HasBeenActivated { get; set; } = ":gear: {0} ({1}) [{2}] активировал SCP-914 на настройке {3}.";

    public string Scp914KnobSettingChanged { get; set; } = ":gear: {0} ({1}) [{2}] поменял настройки SCP-914 на {3}.";

    public string PlayerCanceledWarhead { get; set; } = ":no_entry: **{0} ({1}) [{2}] отменил детонацию альфа-боеголовки.**";

    public string CanceledWarhead { get; set; } = ":no_entry: **Детонация Альфа-боеголвки отменена**";

    public string WarheadHasDetonated { get; set; } = ":radioactive: **Альфа-боеголовка была детонирована.**";

    public string WarheadHasBeenDetonated { get; set; } = "Альфа-боеголовка была детонирована.*";

    public string WarheadIsCountingToDetonation { get; set; } = "Начался отсчет до взрыва альфа-боеголовки.";

    public string WarheadHasntBeenDetonated { get; set; } = "Боеголвка не взорвана.";

    public string PlayerWarheadStarted { get; set; } = ":radioactive: **{0} ({1}) [{2}] запустил альфа-боеголвку, детонация через: {3}.**";

    public string WarheadStarted { get; set; } = ":radioactive: **Alpha-warhead countdown initiated, detonation in: {0}.**";

    public string AccessedWarhead { get; set; } = ":key: {0} ({1}) [{2}] получил доступ к детонации альфа-боеголовки.";

    public string CalledElevator { get; set; } = ":elevator: {0} ({1}) [{2}] вызвал лифт ({3}).";

    public string UsedLocker { get; set; } = "{0} ({1}) [{2}] открыл ящик.";

    public string GeneratorClosed { get; set; } = "{0} ({1}) [{2}] закрыл генератор.";

    public string GeneratorOpened { get; set; } = "{0} ({1}) [{2}] открыл генератор.";

    public string GeneratorEjected { get; set; } = "{0} ({1}) [{2}] отключил запуск генератора.";

    public string GeneratorFinished { get; set; } = "Генератор в {0} начинает свой запуск. {1} генераторов было активировано.";

    public string GeneratorInserted { get; set; } = ":calling: {0} ({1}) [{2}] включил запуск генератора.";

    public string GeneratorUnlocked { get; set; } = ":unlock: {0} ({1}) [{2}] открыл генератор.";

    public string Scp106Teleported { get; set; } = "{0} ({1}) [{2}] использовал способность телепортации.";
    public string Scp106Stalking { get; set; } = "{0} ({1}) [{2}] ушел в измерение.";

    public string GainedExperience { get; set; } = "{0} ({1}) [{2}] получил {3} опыта ({4}).";

    public string GainedLevel { get; set; } = "{0} ({1}) [{2}] получил уровень: {3} :arrow_right: {4}.";

    public string LeftServer { get; set; } = ":arrow_left: **{0} ({1}) [{2}] вышёл с сервера.**";

    public string Reloaded { get; set; } = ":arrows_counterclockwise: {0} ({1}) [{2}] перезарядил {3}.";

    public string GroupSet { get; set; } = "{0} ({1}) [{2}] был привязан к **{3} ({4})** группе.";

    public string ItemChanged { get; set; } = "{0} ({1}) [{2}] поменял вещь в своей руке: {3} :arrow_right: {4}.";

    public string InvalidSubcommand { get; set; } = "Неизвестная подкоманда!";

    public string Available { get; set; } = "Доступно";

    public string BotIsNotConnectedError { get; set; } = "Бот не подключен..";

    public string PlayerListCommandDescription { get; set; } = "Получить список игроков на сервере.";

    public string StaffListCommandDescription { get; set; } = "Получить список админов на сервере.";

    public string ReloadConfigsCommandDescription { get; set; } = "Перезагрузить конфиги бота.";

    public string ReloadConfigsCommandSuccess { get; set; } = "Перезагрузка конфигов завершена..";

    public string NotEnoughPermissions { get; set; } = "Вам нужно \"{0}\" чтобы использовать эту команду..";

    public string ServerConnected { get; set; } = "```diff\n+ Сервер подключен.\n```";

    public string SendingDataError { get; set; } = "Ошибка при отправке данных: {0}";

    public string ReceivingDataError { get; set; } = "Ошибка при получении данных: {0}";

    public string ConnectingError { get; set; } = "Ошибка при подключении: {0}";

    public string SuccessfullyConnected { get; set; } = "Успешно подключен к {0}:{1}.";

    public string ReceivedData { get; set; } = "Получено {0} ({1} байтов) с сервера.";

    public string SentData { get; set; } = "Отправлено {0} ({1} байтов) к серверу.";

    public string ConnectingTo { get; set; } = "Подключение к {0}:{1}.";

    public string ReloadLanguageCommandDescription { get; set; } = "Перезагрузка конфига языка..";

    public string ReloadLanguageCommandSuccess { get; set; } = "Конфиг языка успешно перезагружен!";

    public string ReloadSyncedRolesSuccess { get; set; } = "Bot synced roles reload request sent successfully.";

    public string CouldNotUpdateChannelTopicError { get; set; } = "Ошибка! Не смог обновить тему канала: {0}";

    public string InvalidUserGroupError { get; set; } = "Attempted to assign invalid user group \"{0}\" to {1}.";

    public string AssigningUserGroupError { get; set; } = "Error assigning user group to {0}, player not found.";

    public string AssingingSyncedGroup { get; set; } = "Assigning synced group \"{0}\" to {1}.";

    public string HandlingRemoteCommand { get; set; } = "Handling remote command \"{0}\" with parameters: {1} from {2}.";

    public string HandlingRemoteCommandError { get; set; } = "An error has occurred while handling a remote command: {0}";

    public string None { get; set; } = "None";

    public string InvalidParametersError { get; set; } = "You've to insert {0} parameters!";

    public string AddUserCommandDescription { get; set; } = "Adds an userID-discordID pair to the SyncedRole list.";

    public string AddUserCommandSuccess { get; set; } = "User addition request has been sent successfully.";

    public string AddRoleCommandDescription { get; set; } = "Adds a role-group pair to the SyncedRole list.";

    public string AddRoleCommandSuccess { get; set; } = "Role addition request has been sent successfully.";

    public string RemoveUserCommandDescription { get; set; } = "Removes an userID-discordID pair from the SyncedRole list.";

    public string RemoveUserCommandSuccess { get; set; } = "User deletion request has been sent successfully.";

    public string RemoveRoleCommandDescription { get; set; } = "Removes a role-group pair from the SyncedRole list.";

    public string RemoveRoleCommandSuccess { get; set; } = "Role deletion request has been sent successfully.";

    public string ReloadSyncedRolesDescription { get; set; } = "Reloads bot synced roles if connected.";

    public string InvalidDiscordIdError { get; set; } = "{0} is not a valid Discord ID!";

    public string InvalidUserdIdError { get; set; } = "{0} is not a valid user ID!";

    public string InvalidDiscordRoleIdError { get; set; } = "{0} is not a valid Discord role ID!";

    public string InvalidGroupError { get; set; } = "{0} is not a valid group!";

    public string ServerHasBeenTerminated { get; set; } = "Сервер был уничтожен.";

    public string ServerHasBeenTerminatedWithErrors { get; set; } = "Сервер был уничтожен с ошибкой: {0}";

    public string UpdatingConnectionError { get; set; } = "Ошибка при переподключении: {0}";

    public string InvalidIPAddress { get; set; } = "{0} не правильный айпи-адресс..";

    public string Redacted { get; set; } = "████████";

    public string NotAuthenticated { get; set; } = "Неизвестно - не авторизован..";

    public string DedicatedServer { get; set; } = "Dedicated server";
    public void Load()
    {
        StreamReader streamReader = new StreamReader(FullPath);

        try
        {
            jsonSerializer.Populate(streamReader, this);
        }
        catch (Exception exception)
        {
            Log.Error($"Error! Failed to read {Instance.Config.Language} language, located at \"{FullPath}\": {exception}.\nDefault translation will be used.");
            return;
        }
        finally
        {
            streamReader?.Dispose();
        }
    }
    public void Save(bool shouldOverwrite = false)
    {
        if (File.Exists(FullPath) && !shouldOverwrite)
            return;

        try
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
        }
        catch (Exception exception)
        {
            Log.Error($"Error! Failed to create language directory at \"{Folder}\": {exception}.");
            return;
        }

        StreamWriter streamWriter = new StreamWriter(FullPath);

        try
        {
            jsonSerializer.Serialize(streamWriter, this);
        }
        catch (Exception exception)
        {
            Log.Error($"Error! Failed to create \"{Instance.Config.Language}\" language at \"{FullPath}\": {exception}.");
            return;
        }
        finally
        {
            streamWriter?.Dispose();
        }
    }

    private void Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs ev)
    {
        Log.Warn($"Translation not found for \"{ev.ErrorContext.Member}\" key, loading default one...");

        ev.ErrorContext.Handled = true;
    }
}