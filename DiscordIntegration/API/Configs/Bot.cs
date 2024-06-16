namespace DiscordIntegration.API.Configs
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using API.Commands;
    using Exiled.API.Features;
    using static DiscordIntegration;
    public sealed class Bot
    {
        public static CancellationTokenSource UpdateActivityCancellationTokenSource { get; internal set; }

        [Description("Bot IP address")]
        public string IPAddress { get; private set; } = "127.0.0.1";
        [Description("Bot port")]
        public ushort Port { get; private set; } = 9000;
        [Description("Bot status update interval, in seconds")]
        public float StatusUpdateInterval { get; private set; } = 5;
        [Description("Channel topic update interval, in seconds")]
        public float ChannelTopicUpdateInterval { get; private set; } = 300;
        [Description("Time to wait before trying to reconnect again, in seconds")]
        public float ReconnectionInterval { get; private set; } = 5;
        internal static async Task UpdateActivity(CancellationToken cancellationToken)
        {
            while (true)
            {
                await Network.SendAsync(new RemoteCommand($"updateActivity", $"{Player.Dictionary.Count}/{Instance.Slots}"));
                await Task.Delay(TimeSpan.FromSeconds(Instance.Config.Bot.StatusUpdateInterval), cancellationToken);
            }
        }
    }
}
