using System;
using System.Linq;
using DiscordIntegration.API.Commands;
using DiscordIntegration.API.User;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Scp914;
using PlayerEvent = Exiled.Events.Handlers.Player;
using Scp079Event = Exiled.Events.Handlers.Scp079;
using Scp106Event = Exiled.Events.Handlers.Scp106;
using Scp914Event = Exiled.Events.Handlers.Scp914;
using PlayerRoles;

namespace DiscordIntegration.Events;

using Exiled.API.Features;
using static DiscordIntegration;
internal sealed class PlayerHandler
{
    public PlayerHandler()
    {
        //PlayerEvent.TriggeringTesla += OnTriggeringTesla;
        PlayerEvent.UsedItem += OnUsedMedicalItem;
        PlayerEvent.PickingUpItem += OnPickingUpItem;
        PlayerEvent.ActivatingGenerator += OnInsertingGeneratorTablet;
        PlayerEvent.StoppingGenerator += OnEjectingGeneratorTablet;
        PlayerEvent.UnlockingGenerator += OnUnlockingGenerator;
        PlayerEvent.OpeningGenerator += OnOpeningGenerator;
        PlayerEvent.ClosingGenerator += OnClosingGenerator;
        PlayerEvent.EscapingPocketDimension += OnEscapingPocketDimension;
        PlayerEvent.EnteringPocketDimension += OnEnteringPocketDimension;
        PlayerEvent.ActivatingWarheadPanel += OnActivatingWarheadPanel;
        PlayerEvent.ThrownProjectile += OnThrowingGrenade;
        PlayerEvent.Hurting += OnHurting;
        PlayerEvent.Dying += OnDying;
        PlayerEvent.Kicking += OnKicking;
        PlayerEvent.InteractingDoor += OnInteractingDoor;
        PlayerEvent.InteractingElevator += OnInteractingElevator;
        PlayerEvent.InteractingLocker += OnInteractingLocker;
        PlayerEvent.IntercomSpeaking += OnIntercomSpeaking;
        PlayerEvent.Handcuffing += OnHandcuffing;
        PlayerEvent.RemovingHandcuffs += OnRemovingHandcuffs;
        PlayerEvent.ReloadingWeapon += OnReloadingWeapon;
        PlayerEvent.DroppingItem += OnItemDropped;
        PlayerEvent.Verified += OnVerified;
        PlayerEvent.Destroying += OnDestroying;
        PlayerEvent.ChangingRole += OnChangingRole;
        PlayerEvent.ChangingGroup += OnChangingGroup;
        PlayerEvent.ChangingItem += OnChangingItem;
        PlayerEvent.Banning += OnBanning;
        Scp079Event.InteractingTesla += OnInteractingTesla;
        Scp079Event.GainingLevel += OnGainingLevel;
        Scp079Event.GainingExperience += OnGainingExperience;
        Scp914Event.ChangingKnobSetting += OnChangingScp914KnobSetting;
        Scp914Event.Activating += OnActivatingScp914;
        Scp106Event.Teleporting += OnTeleporting;
        Scp106Event.Stalking += OnStalking;
    }
    ~PlayerHandler()
    {
        //PlayerEvent.TriggeringTesla -= OnTriggeringTesla;
        PlayerEvent.UsedItem -= OnUsedMedicalItem;
        PlayerEvent.PickingUpItem -= OnPickingUpItem;
        PlayerEvent.ActivatingGenerator -= OnInsertingGeneratorTablet;
        PlayerEvent.StoppingGenerator -= OnEjectingGeneratorTablet;
        PlayerEvent.UnlockingGenerator -= OnUnlockingGenerator;
        PlayerEvent.OpeningGenerator -= OnOpeningGenerator;
        PlayerEvent.ClosingGenerator -= OnClosingGenerator;
        PlayerEvent.EscapingPocketDimension -= OnEscapingPocketDimension;
        PlayerEvent.EnteringPocketDimension -= OnEnteringPocketDimension;
        PlayerEvent.ActivatingWarheadPanel -= OnActivatingWarheadPanel;
        PlayerEvent.ThrownProjectile -= OnThrowingGrenade;
        PlayerEvent.Hurting -= OnHurting;
        PlayerEvent.Dying -= OnDying;
        PlayerEvent.Kicking -= OnKicking;
        PlayerEvent.Banning -= OnBanning;
        PlayerEvent.InteractingDoor -= OnInteractingDoor;
        PlayerEvent.InteractingElevator -= OnInteractingElevator;
        PlayerEvent.InteractingLocker -= OnInteractingLocker;
        PlayerEvent.IntercomSpeaking -= OnIntercomSpeaking;
        PlayerEvent.Handcuffing -= OnHandcuffing;
        PlayerEvent.RemovingHandcuffs -= OnRemovingHandcuffs;
        PlayerEvent.ReloadingWeapon -= OnReloadingWeapon;
        PlayerEvent.DroppingItem -= OnItemDropped;
        PlayerEvent.Verified -= OnVerified;
        PlayerEvent.Destroying -= OnDestroying;
        PlayerEvent.ChangingRole -= OnChangingRole;
        PlayerEvent.ChangingGroup -= OnChangingGroup;
        PlayerEvent.ChangingItem -= OnChangingItem;

        Scp079Event.InteractingTesla -= OnInteractingTesla;
        Scp079Event.GainingLevel -= OnGainingLevel;
        Scp079Event.GainingExperience -= OnGainingExperience;
        Scp914Event.ChangingKnobSetting -= OnChangingScp914KnobSetting;
        Scp914Event.Activating -= OnActivatingScp914;

        Scp106Event.Teleporting -= OnTeleporting;
        Scp106Event.Stalking -= OnStalking;
    }
    public async void OnInsertingGeneratorTablet(ActivatingGeneratorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerInsertingGeneratorTablet)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorInserted, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnOpeningGenerator(OpeningGeneratorEventArgs ev)
    {
        if (!ev.IsAllowed) return;
        if (Instance.Config.EventsToLog.PlayerOpeningGenerator)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorOpened, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerUnlockingGenerator)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorUnlocked, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnChangingItem(ChangingItemEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ChangingPlayerItem)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ItemChanged, ev.Player.Nickname, ev.Player.UserId, ev.Player.CurrentItem.Type, ev.NewItem.Type))).ConfigureAwait(false);
    }

    public async void OnGainingExperience(GainingExperienceEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.GainingScp079Experience)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GainedExperience, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Amount, ev.GainType))).ConfigureAwait(false);
    }

    public async void OnGainingLevel(GainingLevelEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.GainingScp079Level)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GainedLevel, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.NewLevel - 1, ev.NewLevel))).ConfigureAwait(false);
    }

    public async void OnDestroying(DestroyingEventArgs ev)
    {
        if (Instance.Config.EventsToLog.PlayerLeft)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.LeftServer, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ReloadingPlayerWeapon)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Reloaded, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Player.CurrentItem.Type))).ConfigureAwait(false);
    }

    public async void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerActivatingWarheadPanel)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.AccessedWarhead, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnInteractingElevator(InteractingElevatorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerInteractingElevator)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.CalledElevator, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Lift.Type))).ConfigureAwait(false);
    }

    public async void OnInteractingLocker(InteractingLockerEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerInteractingLocker)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.UsedLocker, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnClosingGenerator(ClosingGeneratorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerClosingGenerator)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorClosed, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnEjectingGeneratorTablet(StoppingGeneratorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerEjectingGeneratorTablet)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorEjected, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnInteractingDoor(InteractingDoorEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerInteractingDoor && ev.Door.IsKeycardDoor)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(ev.Door.IsOpen ? Language.HasClosedADoor : Language.HasOpenedADoor, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Door.Nametag))).ConfigureAwait(false);
    }

    public async void OnActivatingScp914(ActivatingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ActivatingScp914)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914HasBeenActivated, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Scp914.KnobStatus))).ConfigureAwait(false);
    }

    public async void OnTeleporting(EnteringPocketDimensionEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ActivatingScp914)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914HasBeenActivated, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Scp914.KnobStatus))).ConfigureAwait(false);
    }

    public async void OnChangingScp914KnobSetting(ChangingKnobSettingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.ChangingScp914KnobSetting)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914KnobSettingChanged, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.KnobSetting))).ConfigureAwait(false);
    }

    public async void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerEnteringPocketDimension)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasEnteredPocketDimension, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerEscapingPocketDimension)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasEscapedPocketDimension, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnTeleporting(TeleportingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.Scp106Teleporting)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp106Teleported, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }
    private async void OnStalking(StalkingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.Scp106Stalking)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp106Stalking, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnInteractingTesla(InteractingTeslaEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        // 079 interacting tesla;

        if (Instance.Config.EventsToLog.Scp079InteractingTesla)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.SCP079ActivatedTeslaGate, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }
    /*private async void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
    {

        try
        {
            if (!ev.IsAllowed || !ev.IsTriggerable || ev.DisableTesla) return;

            if (ev.Tesla.PlayersInTriggerRange.Contains(ev.Player)) return;

            if (Instance.Config.EventsToLog.PlayerTriggeringTesla)
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.PlayerTriggeredTeslaGate, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
        } catch(Exception ex)
        {
            Log.Error(ex);
        }
    }*/

    public async void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Player == null || !ev.IsAllowed) return;

        // Я - смерть.
        if ((ev.Player.Health - ev.Amount) <= 0) return;

        if (Instance.Config.EventsToLog.HurtingPlayer && !Instance.Config.BlacklistedDamageTypes.Contains(ev.DamageHandler.Type))
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasDamagedForWith, ev.Attacker?.Nickname ?? "Server", ev.Attacker.UserId, ev.Attacker?.Role ?? RoleTypeId.None, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Amount, ev.DamageHandler.Type))).ConfigureAwait(false);
        
    }

    public async void OnDying(DyingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerDying && ev.Player != null && (ev.Attacker == null || ev.Attacker.Role.Side == ev.Player.Role.Side))
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasKilledWith, ev.Attacker != null ? ev.Attacker.Nickname : "Server", ev.Attacker.UserId, ev.Attacker?.Role ?? RoleTypeId.None, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.DamageHandler.Type))).ConfigureAwait(false);
    }

    public async void OnThrowingGrenade(ThrownProjectileEventArgs ev)
    {
        if (ev.Player != null && Instance.Config.EventsToLog.PlayerThrowingGrenade)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ThrewAGrenade, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Projectile.Type))).ConfigureAwait(false);
    }

    public async void OnUsedMedicalItem(UsedItemEventArgs ev)
    {
        if (ev.Player != null && Instance.Config.EventsToLog.PlayerUsedMedicalItem)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.UsedMedicalItem, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, $"{ev.Item.Type} [{ev.Item.Weight}]"))).ConfigureAwait(false);
    }

    public async void OnChangingRole(ChangingRoleEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (ev.Player != null && Instance.Config.EventsToLog.ChangingPlayerRole)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ChangedRole, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.NewRole))).ConfigureAwait(false);
    }

    public async void OnVerified(VerifiedEventArgs ev)
    {
        if (Instance.Config.ShouldSyncRoles)
        {
            SyncedUser syncedUser = Instance.SyncedUsersCache.FirstOrDefault(tempSyncedUser => tempSyncedUser?.Id == ev.Player.UserId);

            if (syncedUser == null)
                await Network.SendAsync(new RemoteCommand("getGroupFromId", ev.Player.UserId)).ConfigureAwait(false);
            else
                syncedUser?.SetGroup();
        }

        if (Instance.Config.EventsToLog.PlayerJoined)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasJoinedTheGame, ev.Player.Nickname, ev.Player.UserId, ev.Player.IPAddress))).ConfigureAwait(false);
    }

    public async void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerRemovingHandcuffs)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasBeenFreedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnHandcuffing(HandcuffingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.HandcuffingPlayer)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasBeenHandcuffedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnKicking(KickingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        await Network.SendAsync(new RemoteCommand("log", "bans", $":warning: {ev.Target.Nickname} ({ev.Target.UserId}) был кикнут {ev.Player.Nickname} ({ev.Player.UserId}) по причине: {ev.Reason}.")).ConfigureAwait(false);
    }
    public async void OnBanning(BanningEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        await Network.SendAsync(new RemoteCommand("log", "bans", $":no_entry: {ev.Target.Nickname} ({ev.Target.UserId} - {ev.Target.IPAddress}) был забанен {ev.Player.Nickname} ({ev.Player.UserId}) по причине: {ev.Reason}\nдо {DateTime.Now.AddSeconds(ev.Duration).ToString("HH:mm:ss - dd.MM.yyyy")}"));
    }
    public async void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
    {
        if (ev.Player == null) return;
        if (!ev.IsAllowed || ev.Player.IsIntercomMuted) return;
        if (ev.Player.VoiceChannel == VoiceChat.VoiceChatChannel.Intercom) return;

        if (Instance.Config.EventsToLog.PlayerIntercomSpeaking)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasStartedUsingTheIntercom, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role))).ConfigureAwait(false);
    }

    public async void OnPickingUpItem(PickingUpItemEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerPickingupItem)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasPickedUp, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Pickup.Type))).ConfigureAwait(false);
    }

    public async void OnItemDropped(DroppingItemEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Instance.Config.EventsToLog.PlayerItemDropped)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasDropped, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Item.Type))).ConfigureAwait(false);
    }

    public async void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (ev.Player != null && Instance.Config.EventsToLog.ChangingPlayerGroup)
            await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GroupSet, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.NewGroup?.BadgeText ?? Language.None, ev.NewGroup?.BadgeColor ?? Language.None))).ConfigureAwait(false);
    }
}