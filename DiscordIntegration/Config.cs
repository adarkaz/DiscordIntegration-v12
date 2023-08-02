using System.Collections.Generic;
using System.ComponentModel;
using DiscordIntegration.API.Configs;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace DiscordIntegration;
public sealed class Config : IConfig
{
    [Description("Определяет включён плагин или нет.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Определяет включён дебаг-мод или нет.")]
    public bool Debug { get; set; } = false;
    public Bot Bot { get; private set; } = new Bot();
    [Description("Ивенты для логирования..")]
    public EventsToLog EventsToLog { get; private set; } = new EventsToLog();
    public List<DamageType> BlacklistedDamageTypes { get; private set; } = new List<DamageType>()
    {
            DamageType.Scp207,
            DamageType.PocketDimension,
    };
    public bool OnlyLogPlayerDamage { get; private set; }
    public string DateFormat { get; private set; } = "dd/MM/yy HH:mm:ss";
    public bool ShouldSyncRoles { get; private set; } = true;
    public bool IsDebugEnabled { get; private set; }
    public string Language { get; private set; } = "english";
}