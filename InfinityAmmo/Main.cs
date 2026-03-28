using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Events.Handlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;

namespace InfinityAmmo
{
    public class Main : Plugin
    {
        public override void Enable()
        {
            PlayerEvents.ReloadingWeapon += OnReloadingWeapon;
            PlayerEvents.DroppingAmmo += OnDroppingAmmo;
        }

        public override void Disable()
        {
            PlayerEvents.ReloadingWeapon -= OnReloadingWeapon;
            PlayerEvents.DroppingAmmo -= OnDroppingAmmo;
        }

        private void OnReloadingWeapon(PlayerReloadingWeaponEventArgs ev)
        {
            if (ev.FirearmItem.IsReloadingOrUnloading)
                return;
            
            if (ev.FirearmItem.StoredAmmo != ev.FirearmItem.MaxAmmo)
            {
                int ammo = ev.FirearmItem.MaxAmmo - ev.FirearmItem.StoredAmmo;
                ev.Player.SetAmmo(ev.FirearmItem.AmmoType, (ushort)ammo);
                ev.FirearmItem.Reload();
            }
        }

        private void OnDroppingAmmo(PlayerDroppingAmmoEventArgs ev)
        {
            ev.IsAllowed = false;
        }
        
        public override string Name { get; } = "InfinityAmmo";
        public override string Description { get; } = "InfinityAmmo";
        public override string Author { get; } = "AgTeam";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredApiVersion { get; } = LabApiProperties.CurrentVersion;
    }
}