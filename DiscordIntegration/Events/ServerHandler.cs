using DiscordIntegration.API.Commands;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Respawning;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace DiscordIntegration.Events;

using static DiscordIntegration;
internal sealed class ServerHandler
{
    public ServerHandler()
    {
        ServerEvent.WaitingForPlayers += OnWaitingForPlayers;
        ServerEvent.RoundStarted += OnRoundStarted;
        ServerEvent.RoundEnded += OnRoundEnded;
        ServerEvent.RespawningTeam += OnRespawningTeam;
        ServerEvent.ReportingCheater += OnReportingCheater;
        ServerEvent.LocalReporting += OnLocalReporting;
        ServerEvent.RestartingRound += OnRestarting;
    }
    ~ServerHandler()
    {
        ServerEvent.WaitingForPlayers -= OnWaitingForPlayers;
        ServerEvent.RoundStarted -= OnRoundStarted;
        ServerEvent.RoundEnded -= OnRoundEnded;
        ServerEvent.RespawningTeam -= OnRespawningTeam;
        ServerEvent.ReportingCheater -= OnReportingCheater;
        ServerEvent.LocalReporting -= OnLocalReporting;
        ServerEvent.RestartingRound -= OnRestarting;
    }
    public async void OnReportingCheater(ReportingCheaterEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ReportingCheater)
            await Network.SendAsync(new RemoteCommand("log", "reports", string.Format(Language.ReportFilled, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Reason))).ConfigureAwait(false);
    }

    public async void OnLocalReporting(LocalReportingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ReportingCheater)
            await Network.SendAsync(new RemoteCommand("log", "reports", string.Format(Language.CheaterReportFilled, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Reason))).ConfigureAwait(false);
    }

    public async void OnWaitingForPlayers()
    {
        if (Instance.Config.EventsToLog.WaitingForPlayers)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", Language.WaitingForPlayers)).ConfigureAwait(false);
    }
    public async void OnRestarting()
    {
        if (Instance.Config.EventsToLog.RestartingServer)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", Language.RestartingServer)).ConfigureAwait(false);
    }
    public async void OnRoundStarted()
    {
        if (Instance.Config.EventsToLog.RoundStarted)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.RoundStarting, Player.Dictionary.Count))).ConfigureAwait(false);
    }

    public async void OnRoundEnded(RoundEndedEventArgs ev)
    {
        if (Instance.Config.EventsToLog.RoundEnded)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.RoundEnded, ev.LeadingTeam, Player.Dictionary.Count, Instance.Slots))).ConfigureAwait(false);
    }

    public async void OnRespawningTeam(RespawningTeamEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.RespawningTeam)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(ev.NextKnownTeam == SpawnableTeamType.ChaosInsurgency ? Language.ChaosInsurgencyHaveSpawned : Language.NineTailedFoxHaveSpawned, ev.Players.Count))).ConfigureAwait(false);
    }
}