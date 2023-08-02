// -----------------------------------------------------------------------
// <copyright file="StaffList.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace DiscordIntegration.Commands
{
    using System;
    using System.Text;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using NorthwoodLib.Pools;
    using static DiscordIntegration;
    internal sealed class StaffList : ICommand
    {
        private StringBuilder StringBuilder = new();

        public static StaffList Instance { get; } = new StaffList();

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
                if (!player.RemoteAdminAccess || player.Group == null || player.GroupName == null) continue;

                StringBuilder.Append(player.Nickname);
                StringBuilder.Append(" - ");
                StringBuilder.Append($"{player.UserId} ({player.Id})");
                StringBuilder.Append(" - ");
                StringBuilder.Append(player.GroupName).AppendLine();
            }

            if (StringBuilder.Length == 0)
                StringBuilder.Append(Language.NoStaffOnline);

            response = StringBuilder.ToString();
            return true;
        }
    }
}
