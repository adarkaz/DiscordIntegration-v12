namespace DiscordIntegration.Commands
{
    using System;
    using System.Text;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using static DiscordIntegration;
    internal sealed class StaffList : ICommand
    {
        private StringBuilder StringBuilder = new();
        public bool SanitizeResponse { get; } = false;
        public string Command { get; } = "stafflist";

        public string[] Aliases { get; } = new[] { "sl" };

        public string Description { get; } = Language.StaffListCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("di.stafflist"))
            {
                response = string.Format(Language.NotEnoughPermissions, "di.stafflist");
                return false;
            }

            StringBuilder.Clear();
            StringBuilder.AppendLine();

            foreach (Player player in Player.List)
            {
                if (!player.RemoteAdminAccess || player.Group == null) continue;

                StringBuilder.Append($"`{player.Nickname} - {player.UserId} ({player.Id}) - {player.Group.BadgeText}`").AppendLine();
            }

            if (StringBuilder.Length == 0)
                StringBuilder.Append(Language.NoStaffOnline);

            response = StringBuilder.ToString();
            return true;
        }
    }
}
