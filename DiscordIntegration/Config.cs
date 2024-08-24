using System.Collections.Generic;
using System.ComponentModel;
using DiscordIntegration.API.Configs;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace DiscordIntegration;
public class Config : IConfig
{
    [Description("Îïðåäåëÿåò âêëþ÷¸í ïëàãèí èëè íåò.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Îïðåäåëÿåò âêëþ÷¸í äåáàã-ìîä èëè íåò.")]
    public bool Debug { get; set; } = false;
    public Bot Bot { get; private set; } = new Bot();
    [Description("Èâåíòû äëÿ ëîãèðîâàíèÿ..")]
    public EventsToLog EventsToLog { get; private set; } = new EventsToLog();
    public List<DamageType> BlacklistedDamageTypes { get; private set; } = new List<DamageType>()
    {
            DamageType.Scp207,
            DamageType.PocketDimension,
    };
    public bool ShouldSyncRoles { get; private set; } = true;
    public bool IsDebugEnabled { get; private set; }
    public string Language { get; private set; } = "russian";
}