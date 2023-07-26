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

        [Description("�������� �� ������?")]
        public bool IsEnabled { get; set; } = true;

        [Description("�������� �� �����-���?")]
        public bool Debug { get; set; } = false;

        public Bot Bot { get; private set; } = new Bot();
        [Description("������ ��� �����������..")]
        public EventsToLog EventsToLog { get; private set; } = new EventsToLog();
        [Description("����� �� ���������� ����-������?")]
        public bool ShouldLogIPAddresses { get; private set; } = true;
        [Description("����� �� ���������� ����-���� �����/��������?")]
        public bool ShouldLogUserIds { get; private set; } = true;
        [Description("����� �� ������� DNT?")]
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