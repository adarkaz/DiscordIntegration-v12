// -----------------------------------------------------------------------
// <copyright file="PlayerHandler.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace DiscordIntegration.Events
{
    using System;
    using System.Linq;
    using API.Commands;
    using API.User;
    using Exiled.API;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.API.Features.DamageHandlers;
    using Exiled.Events.EventArgs;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Scp079;
    using Exiled.Events.EventArgs.Scp106;
    using Exiled.Events.EventArgs.Scp914;
    using PlayerRoles;
    using static DiscordIntegration;

    /// <summary>
    /// Handles player-related events.
    /// </summary>
    internal sealed class PlayerHandler
    {
#pragma warning disable SA1600 // Elements should be documented
        public async void OnInsertingGeneratorTablet(ActivatingGeneratorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerInsertingGeneratorTablet && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorInserted, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnOpeningGenerator(OpeningGeneratorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerOpeningGenerator && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorOpened, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerUnlockingGenerator && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorUnlocked, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (Instance.Config.EventsToLog.ChangingPlayerItem && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ItemChanged, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.CurrentItem.Type, ev.NewItem.Type))).ConfigureAwait(false);
        }

        public async void OnGainingExperience(GainingExperienceEventArgs ev)
        {
            if (Instance.Config.EventsToLog.GainingScp079Experience && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GainedExperience, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Amount, ev.GainType))).ConfigureAwait(false);
        }

        public async void OnGainingLevel(GainingLevelEventArgs ev)
        {
            if (Instance.Config.EventsToLog.GainingScp079Level && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GainedLevel, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.NewLevel - 1, ev.NewLevel))).ConfigureAwait(false);
        }

        public async void OnDestroying(DestroyingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerLeft && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.LeftServer, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
        {
            if (Instance.Config.EventsToLog.ReloadingPlayerWeapon && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Reloaded, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Player.CurrentItem.Type))).ConfigureAwait(false);
        }

        public async void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerActivatingWarheadPanel && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.AccessedWarhead, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerInteractingElevator && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.CalledElevator, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Lift.Type))).ConfigureAwait(false);
        }

        public async void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerInteractingLocker && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.UsedLocker, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnClosingGenerator(ClosingGeneratorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerClosingGenerator && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorClosed, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnEjectingGeneratorTablet(StoppingGeneratorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerEjectingGeneratorTablet && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GeneratorEjected, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerInteractingDoor && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(ev.Door.IsOpen ? Language.HasClosedADoor : Language.HasOpenedADoor, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Door.Nametag))).ConfigureAwait(false);
        }

        public async void OnActivatingScp914(ActivatingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.ActivatingScp914 && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914HasBeenActivated, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, Scp914.KnobStatus))).ConfigureAwait(false);
        }

        public async void OnTeleporting(EnteringPocketDimensionEventArgs ev)
        {
            if (Instance.Config.EventsToLog.ActivatingScp914 && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914HasBeenActivated, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, Scp914.KnobStatus))).ConfigureAwait(false);
        }

        public async void OnChangingScp914KnobSetting(ChangingKnobSettingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.ChangingScp914KnobSetting && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp914KnobSettingChanged, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.KnobSetting))).ConfigureAwait(false);
        }

        public async void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerEnteringPocketDimension && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasEnteredPocketDimension, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerEscapingPocketDimension && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasEscapedPocketDimension, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnTeleporting(TeleportingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.Scp106Teleporting && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.Scp106Teleported, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (Instance.Config.EventsToLog.Scp079InteractingTesla && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasTriggeredATeslaGate, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnHurting(HurtingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.HurtingPlayer && ev.Player != null && (ev.Attacker == null || ev.Attacker.Role.Side == ev.Player.Role.Side) && (!Instance.Config.ShouldRespectDoNotTrack || (ev.Attacker == null || (!ev.Attacker.DoNotTrack && !ev.Player.DoNotTrack))) && !Instance.Config.BlacklistedDamageTypes.Contains(ev.DamageHandler.Type) && (!Instance.Config.OnlyLogPlayerDamage || ev.Attacker != null))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasDamagedForWith, ev.Attacker != null ? ev.Attacker.Nickname : "Server", Instance.Config.ShouldLogUserIds ? ev.Attacker != null ? ev.Attacker.UserId : string.Empty : Language.Redacted, ev.Attacker?.Role ?? RoleTypeId.None, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Amount, ev.DamageHandler.Type))).ConfigureAwait(false);
        }

        public async void OnDying(DyingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerDying && ev.Player != null && (ev.Attacker == null || ev.Attacker.Role.Side == ev.Player.Role.Side) && (!Instance.Config.ShouldRespectDoNotTrack || (ev.Attacker == null || (!ev.Attacker.DoNotTrack && !ev.Attacker.DoNotTrack))))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasKilledWith, ev.Attacker != null ? ev.Attacker.Nickname : "Server", Instance.Config.ShouldLogUserIds ? ev.Attacker != null ? ev.Attacker.UserId : string.Empty : Language.Redacted, ev.Attacker?.Role ?? RoleTypeId.None, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.DamageHandler.Type))).ConfigureAwait(false);
        }

        public async void OnThrowingGrenade(ThrownProjectileEventArgs ev)
        {
            if (ev.Player != null && Instance.Config.EventsToLog.PlayerThrowingGrenade && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ThrewAGrenade, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Projectile.Type))).ConfigureAwait(false);
        }

        public async void OnUsedMedicalItem(UsedItemEventArgs ev)
        {
            if (ev.Player != null && Instance.Config.EventsToLog.PlayerUsedMedicalItem && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.UsedMedicalItem, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Item))).ConfigureAwait(false);
        }

        public async void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player != null && Instance.Config.EventsToLog.ChangingPlayerRole && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.ChangedRole, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.NewRole))).ConfigureAwait(false);
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

            if (Instance.Config.EventsToLog.PlayerJoined && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasJoinedTheGame, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, Instance.Config.ShouldLogIPAddresses ? ev.Player.IPAddress : Language.Redacted))).ConfigureAwait(false);
        }

        public async void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerRemovingHandcuffs && ((!ev.Player.DoNotTrack && !ev.Target.DoNotTrack) || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasBeenFreedBy, ev.Target.Nickname, Instance.Config.ShouldLogUserIds ? ev.Target.UserId : Language.Redacted, ev.Target.Role, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (Instance.Config.EventsToLog.HandcuffingPlayer && ((!ev.Player.DoNotTrack && !ev.Target.DoNotTrack) || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasBeenHandcuffedBy, ev.Target.Nickname, Instance.Config.ShouldLogUserIds ? ev.Target.UserId : Language.Redacted, ev.Target.Role, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnKicked(KickedEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerBanned)
                await Network.SendAsync(new RemoteCommand("log", "kicks", string.Format(Language.WasKicked, ev.Player.Nickname ?? Language.NotAuthenticated, ev.Player?.UserId ?? Language.NotAuthenticated, ev.Reason))).ConfigureAwait(false);
        }

        public async void OnBanned(BannedEventArgs ev)
        {
            try
            {
                await Network.SendAsync(new RemoteCommand("log", "bans", $":no_entry: {ev.Details.OriginalName} ({ev.Details.Id}) был забанен {ev.Details.Issuer} за {ev.Details.Reason} до {new DateTime(ev.Details.Expires).ToString(Instance.Config.DateFormat)}."));
            }
            catch (Exception x)
            {
                Log.Error(x);
            }
        }

        public async void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            if (ev.Player != null && Instance.Config.EventsToLog.PlayerIntercomSpeaking && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasStartedUsingTheIntercom, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role))).ConfigureAwait(false);
        }

        public async void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerPickingupItem && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasPickedUp, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Pickup.Type))).ConfigureAwait(false);
        }

        public async void OnItemDropped(DroppingItemEventArgs ev)
        {
            if (Instance.Config.EventsToLog.PlayerItemDropped && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.HasDropped, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.Item.Type))).ConfigureAwait(false);
        }

        public async void OnChangingGroup(ChangingGroupEventArgs ev)
        {
            if (ev.Player != null && Instance.Config.EventsToLog.ChangingPlayerGroup && (!ev.Player.DoNotTrack || !Instance.Config.ShouldRespectDoNotTrack))
                await Network.SendAsync(new RemoteCommand("log", "gameEvents", string.Format(Language.GroupSet, ev.Player.Nickname, Instance.Config.ShouldLogUserIds ? ev.Player.UserId : Language.Redacted, ev.Player.Role, ev.NewGroup?.BadgeText ?? Language.None, ev.NewGroup?.BadgeColor ?? Language.None))).ConfigureAwait(false);
        }
    }
}