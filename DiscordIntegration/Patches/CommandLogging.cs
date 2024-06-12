using System;
using Exiled.API.Features;
using DiscordIntegration.API.Commands;
using HarmonyLib;
using RemoteAdmin;

namespace DiscordIntegration.Patches;

[HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
internal class CommandLogging
{
    [HarmonyPostfix]
    private static async void LogCommand(string q, CommandSender sender, string __result)
    {
        string[] args = q.Trim().Split(QueryProcessor.SpaceArray, 512, StringSplitOptions.RemoveEmptyEntries);

        if (args[0].StartsWith("$"))
            return;

        Player player = sender is RemoteAdmin.PlayerCommandSender playerCommandSender
            ? Player.Get(playerCommandSender)
            : Server.Host;

        if (player == null)
            return;

        string CommandInputText = q.Trim();

        //if (CommandInputText.Length > 1000) CommandInputText = $"{(args[0].Length < 250 ? args[0] : "������� �������")} � ��� {CommandInputText.Length - args[0].Length} ��������.. <@675714186898309133> -> {(string.IsNullOrEmpty(__result) ? "������� ������ �� ������" : __result)}";
        await DiscordIntegration.Network.SendAsync(new RemoteCommand("log", "commands", $":keyboard: {sender.Nickname} ({sender.SenderId ?? DiscordIntegration.Language.DedicatedServer}) ����������� �������: {CommandInputText} -> {(string.IsNullOrEmpty(__result) ? "������� ������ �� ������" : $"{__result.Replace('\n', ' ')}")}"));
    }
}