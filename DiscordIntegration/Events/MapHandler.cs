using System;
using System.Linq;
using DiscordIntegration.API.Commands;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Scp914;
using Exiled.Events.EventArgs.Warhead;
using DiscordIntegration.API;
using Handlers = Exiled.Events.Handlers;

namespace DiscordIntegration.Events;

using static DiscordIntegration;

internal sealed class MapHandler
{
    public MapHandler()
    {
        Handlers.Map.Decontaminating += OnDecontaminating;
        Handlers.Map.GeneratorActivating += OnGeneratorActivated;

        Handlers.Scp914.UpgradingInventoryItem += OnUpgradingItems;

        Handlers.Warhead.Starting += OnStartingWarhead;
        Handlers.Warhead.Stopping += OnStoppingWarhead;
        Handlers.Warhead.Detonated += OnWarheadDetonated;
    }
    ~MapHandler()
    {
        Handlers.Map.Decontaminating -= OnDecontaminating;
        Handlers.Map.GeneratorActivating -= OnGeneratorActivated;
        Handlers.Warhead.Starting -= OnStartingWarhead;
        Handlers.Warhead.Stopping -= OnStoppingWarhead;
        Handlers.Warhead.Detonated -= OnWarheadDetonated;
        Handlers.Scp914.UpgradingInventoryItem -= OnUpgradingItems;
    }
    public async void OnWarheadDetonated()
    {
        if (Instance.Config.EventsToLog.WarheadDetonated)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", Language.WarheadHasDetonated)).ConfigureAwait(false);
    }
    public async void OnGeneratorActivated(GeneratorActivatingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.GeneratorActivated)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorFinished, ev.Generator.Room, Generator.Get(GeneratorState.Engaged).Count() + 1))).ConfigureAwait(false);
    }
    public async void OnDecontaminating(DecontaminatingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.Decontaminating)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", Language.DecontaminationHasBegun)).ConfigureAwait(false);
    }

    public async void OnStartingWarhead(StartingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.StartingWarhead && (ev.Player == null || (ev.Player != null)))
        {
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.PlayerWarheadStarted, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Warhead.DetonationTimer))).ConfigureAwait(false);
        }
    }

    public async void OnStoppingWarhead(StoppingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.StoppingWarhead && (ev.Player == null || (ev.Player != null)))
        {
            object[] vars = ev.Player == null ?
                Array.Empty<object>() :
                new object[] { ev.Player.Nickname, ev.Player.UserId, ev.Player.Role.Type };

            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(ev.Player == null ? Language.CanceledWarhead : Language.PlayerCanceledWarhead, vars))).ConfigureAwait(false);
        }
    }

    public async void OnUpgradingItems(UpgradingInventoryItemEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.UpgradingScp914Items)
        {
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914ProcessedItem, ev.Item.Type)));
        }
    }
}