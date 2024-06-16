using System;
using System.Collections.Generic;
using System.Threading;
using Exiled.API.Features;
using HarmonyLib;
using MEC;

namespace DiscordIntegration;

using API;
using API.Configs;
using API.User;
using Exiled.API.Enums;

public class DiscordIntegration : Plugin<Config>
{
    public override string Author => "swd";
    public override string Name => "DiscordIntegration";
    public override Version Version => new(1, 0, 0);
    private static readonly DiscordIntegration InstanceValue = new();
    private readonly List<CoroutineHandle> coroutines = new();

    private Events.MapHandler MapHandler;
    private Events.ServerHandler ServerHandler;
    private Events.PlayerHandler PlayerHandler;
    private Events.NetworkHandler NetworkHandler;

    private Harmony harmony;
    public static Language Language { get; private set; }
    public static Network Network { get; private set; }
    public static CancellationTokenSource NetworkCancellationTokenSource { get; private set; }
    public static DiscordIntegration Instance => InstanceValue;
    public override PluginPriority Priority => PluginPriority.Last;
    public HashSet<SyncedUser> SyncedUsersCache { get; } = new HashSet<SyncedUser>();
    public int Slots => CustomNetworkManager.slots;
    public override void OnEnabled()
    {
        
        harmony = new Harmony($"DiscordIntegration - {DateTime.Now.Ticks}");
        harmony.PatchAll();

        Language = new Language();
        Network = new Network(Instance.Config.Bot.IPAddress, Instance.Config.Bot.Port, TimeSpan.FromSeconds(Instance.Config.Bot.ReconnectionInterval));

        NetworkCancellationTokenSource = new CancellationTokenSource();

        Language.Save();
        Language.Load();

        MapHandler = new();
        ServerHandler = new();
        PlayerHandler = new();
        NetworkHandler = new();

        Bot.UpdateActivityCancellationTokenSource = new CancellationTokenSource();

        _ = Network.Start(NetworkCancellationTokenSource.Token);

        _ = Bot.UpdateActivity(Bot.UpdateActivityCancellationTokenSource.Token);

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        harmony.UnpatchAll();
        KillCoroutines();

        NetworkCancellationTokenSource.Cancel();
        NetworkCancellationTokenSource.Dispose();

        Network.Close();

        Bot.UpdateActivityCancellationTokenSource.Cancel();
        Bot.UpdateActivityCancellationTokenSource.Dispose();

        SyncedUsersCache.Clear();

        MapHandler = null;
        ServerHandler = null;
        PlayerHandler = null;
        NetworkHandler = null;

        Language = null;
        Network = null;

        base.OnDisabled();
    }
    private void KillCoroutines()
    {
        Timing.KillCoroutines(coroutines.ToArray());

        coroutines.Clear();
    }

}