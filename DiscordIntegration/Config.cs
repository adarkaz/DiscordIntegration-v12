namespace DiscordIntegration
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using API.Configs;
    using Exiled.API.Enums;
    using Exiled.API.Interfaces;

    /// <inheritdoc cref="IConfig"/>
    public sealed class Config : IConfig
    {
#pragma warning disable SA1600
#pragma warning disable SA1516

        [Description("Включать ли плагин?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Включать ли дебаг-мод?")]
        public bool Debug { get; set; } = false;

        public Bot Bot { get; private set; } = new Bot();
        [Description("Ивенты для логирования..")]
        public EventsToLog EventsToLog { get; private set; } = new EventsToLog();
        [Description("Стоит ли логировать айпи-адресс?")]
        public bool ShouldLogIPAddresses { get; private set; } = true;
        [Description("Стоит ли логировать юзер-айди стима/дискорда?")]
        public bool ShouldLogUserIds { get; private set; } = true;
        [Description("Стоит ли уважать DNT?")]
        public bool ShouldRespectDoNotTrack { get; private set; } = true;
        public List<DamageType> BlacklistedDamageTypes { get; private set; } = new List<DamageType>
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
}