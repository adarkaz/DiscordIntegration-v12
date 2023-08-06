using CommandSystem;
using System.Text.RegularExpressions;

namespace DiscordIntegration;
using API.Commands;
public static class Extensions
{
    public static bool IsValidUserId(this string userId) => Regex.IsMatch(userId, "^([0-9]{17})@(steam|patreon|northwood)|([0-9]{19})@(discord)$");
    public static bool IsValidDiscordId(this string discordId) => Regex.IsMatch(discordId, "^[0-9]{18,19}$");
    public static bool IsValidDiscordRoleId(this string discordRoleId) => IsValidDiscordId(discordRoleId);
    public static CommandSender GetCompatible(this ICommandSender sender) => ((CommandSender)sender).GetCompatible();
    public static CommandSender GetCompatible(this CommandSender sender)
    {
        if (sender.GetType() != typeof(RemoteAdmin.PlayerCommandSender))
            return sender;

        return new PlayerCommandSender(sender.SenderId, sender.Nickname, sender.Permissions, sender.KickPower, sender.FullPermissions);
    }
}