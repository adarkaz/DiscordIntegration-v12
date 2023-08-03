using System;
using System.Net;
using DiscordIntegration.API.Commands;
using DiscordIntegration.API.EventArgs.Network;
using DiscordIntegration.API.User;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace DiscordIntegration.Events;

using static DiscordIntegration;

internal sealed class NetworkHandler
{
    public NetworkHandler()
    {
        Network.SendingError += OnSendingError;
        Network.ReceivingError += OnReceivingError;
        Network.UpdatingConnectionError += OnUpdatingConnectionError;
        Network.ConnectingError += OnConnectingError;
        Network.Connected += OnConnected;
        Network.Connecting += OnConnecting;
        Network.ReceivedFull += OnReceivedFull;
        Network.Terminated += OnTerminated;
    }
    ~NetworkHandler()
    {
        Network.SendingError -= OnSendingError;
        Network.ReceivingError -= OnReceivingError;
        Network.UpdatingConnectionError -= OnUpdatingConnectionError;
        Network.ConnectingError -= OnConnectingError;
        Network.Connected -= OnConnected;
        Network.Connecting -= OnConnecting;
        Network.ReceivedFull -= OnReceivedFull;
        Network.Terminated -= OnTerminated;
    }
    public void OnReceivedFull(object _, ReceivedFullEventArgs ev)
    {
        try
        {
            RemoteCommand remoteCommand = JsonConvert.DeserializeObject<RemoteCommand>(ev.Data, Network.JsonSerializerSettings);

            switch (remoteCommand.Action)
            {
                case "executeCommand":
                    JsonConvert.DeserializeObject<GameCommand>(remoteCommand.Parameters[0].ToString())?.Execute();
                    break;
                case "setGroupFromId":
                    SyncedUser syncedUser = JsonConvert.DeserializeObject<SyncedUser>(remoteCommand.Parameters[0].ToString(), Network.JsonSerializerSettings);

                    if (syncedUser == null)
                        break;

                    if (!Instance.SyncedUsersCache.Contains(syncedUser))
                        Instance.SyncedUsersCache.Add(syncedUser);

                    syncedUser?.SetGroup();
                    break;
                case "commandReply":
                    JsonConvert.DeserializeObject<CommandReply>(remoteCommand.Parameters[0].ToString(), Network.JsonSerializerSettings)?.Answer();
                    break;
            }
        }
        catch (Exception exception)
        {
            Log.Error($"[NET] {string.Format(Language.HandlingRemoteCommandError, Instance.Config.IsDebugEnabled ? exception.ToString() : exception.Message)}");
        }
    }

    /// <inheritdoc cref="API.Network.OnSendingError(object, SendingErrorEventArgs)"/>
    public void OnSendingError(object _, SendingErrorEventArgs ev)
    {
        Log.Debug($"[NET] {string.Format(Language.SendingDataError, Instance.Config.IsDebugEnabled ? ev.Exception.ToString() : ev.Exception.Message)}");
    }

    /// <inheritdoc cref="API.Network.OnReceivingError(object, ReceivingErrorEventArgs)"/>
    public void OnReceivingError(object _, ReceivingErrorEventArgs ev)
    {
        Log.Error($"[NET] {string.Format(Language.ReceivingDataError, Instance.Config.IsDebugEnabled ? ev.Exception.ToString() : ev.Exception.Message)}");
    }

    /// <inheritdoc cref="API.Network.OnSent(object, SentEventArgs)"/>
    /// <inheritdoc cref="API.Network.OnConnecting(object, ConnectingEventArgs)"/>
    public void OnConnecting(object _, ConnectingEventArgs ev)
    {
        if (!IPAddress.TryParse(Instance.Config?.Bot?.IPAddress, out IPAddress ipAddress))
        {
            Log.Error($"[NET] {string.Format(Language.InvalidIPAddress, Instance.Config?.Bot?.IPAddress)}");
            return;
        }

        ev.IPAddress = ipAddress;
        ev.Port = Instance.Config.Bot.Port;
        ev.ReconnectionInterval = TimeSpan.FromSeconds(Instance.Config.Bot.ReconnectionInterval);

        Log.Debug($"[NET] {string.Format(Language.ConnectingTo, ev.IPAddress, ev.Port)}");
    }

    /// <inheritdoc cref="API.Network.OnConnected(object, System.EventArgs)"/>
    public async void OnConnected(object _, System.EventArgs ev)
    {
        Log.Info($"[NET] {string.Format(Language.SuccessfullyConnected, Network.IPEndPoint?.Address, Network.IPEndPoint?.Port)}");

        await Network.SendAsync(new RemoteCommand("log", "gameEvents", Language.ServerConnected, true));
    }

    /// <inheritdoc cref="API.Network.OnConnectingError(object, ConnectingErrorEventArgs)"/>
    public void OnConnectingError(object _, ConnectingErrorEventArgs ev)
    {
        Log.Error($"[NET] {string.Format(Language.ConnectingError, Instance.Config.IsDebugEnabled ? ev.Exception.ToString() : ev.Exception.Message)}");
    }

    /// <inheritdoc cref="API.Network.OnConnectingError(object, ConnectingErrorEventArgs)"/>
    public void OnUpdatingConnectionError(object _, UpdatingConnectionErrorEventArgs ev)
    {
        Log.Error($"[NET] {string.Format(Language.UpdatingConnectionError, Instance.Config.IsDebugEnabled ? ev.Exception.ToString() : ev.Exception.Message)}");
    }

    /// <inheritdoc cref="API.Network.OnTerminated(object, TerminatedEventArgs)"/>
    public void OnTerminated(object _, TerminatedEventArgs ev)
    {
        if (ev.Task.IsFaulted)
            Log.Error($"[NET] {string.Format(Language.ServerHasBeenTerminatedWithErrors, Instance.Config.IsDebugEnabled ? ev.Task.Exception.ToString() : ev.Task.Exception.Message)}");
        else
            Log.Warn($"[NET] {Language.ServerHasBeenTerminated}");
    }
}