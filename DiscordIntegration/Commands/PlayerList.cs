namespace DiscordIntegration.Commands;

using System;
using System.Text;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using static DiscordIntegration;
public class PlayerList : ICommand
{
    public bool SanitizeResponse { get; } = false;
    public string Command { get; } = "playerlist";

    public string[] Aliases { get; } = new[] { "pli" };

    public string Description { get; } = Language.PlayerListCommandDescription;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("di.playerlist"))
        {
            response = string.Format(Language.NotEnoughPermissions, "di.playerlist");
            return false;
        }

        StringBuilder message = StringBuilderPool.Shared.Rent();

        message.Append('\n');

        if (Player.Dictionary.Count == 0)
        {
            message.Append(Language.NoPlayersOnline);
        }
        else
        {
            foreach (Player player in Player.List)
                message.Append($"`{player.Nickname} - {player.UserId} ({player.Id})`").AppendLine();
        }

        response = message.ToString();

        StringBuilderPool.Shared.Return(message);

        return true;
    }
}
