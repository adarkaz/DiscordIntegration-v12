namespace DiscordIntegration.API.Commands
{
    using Exiled.API.Features;
    using Newtonsoft.Json;
    public class PlayerCommandSender : CommandSender
    {
        private readonly Player player;
        [JsonConstructor]
        public PlayerCommandSender(string senderId, string nickname, ulong permissions, byte kickPower, bool fullPermissions)
        {
            SenderId = senderId;
            Nickname = nickname;
            Permissions = permissions;
            KickPower = kickPower;
            FullPermissions = fullPermissions;

            player = Player.Get(SenderId);
        }

        public override string SenderId { get; }
        public override string Nickname { get; }
        public override ulong Permissions { get; }
        public override byte KickPower { get; }
        public override bool FullPermissions { get; }
        public override void Print(string text) => player.Sender.Print(text);
        public override void RaReply(string text, bool success, bool logToConsole, string overrideDisplay) => player.Sender.RaReply($"DISCORDINTEGRATION#{text}", success, logToConsole, overrideDisplay);
    }
}
