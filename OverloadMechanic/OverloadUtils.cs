using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OverloadMechanic
{
    public static class OverloadUtils
    {

        //Ammo save caused by weapon animation rather than actual formula
        public static bool IsAmmoFreeFromAnimation(Player player, Item weapon, Item ammo)
        {
            if (!CombinedHooks.CanConsumeAmmo(player, weapon, ammo))
                return true;

            if (weapon.consumeAmmoOnFirstShotOnly && !player.ItemAnimationJustStarted)
            {
                return true;
            }

            if (weapon.consumeAmmoOnLastShotOnly)
            {
                int useTime = CombinedHooks.TotalUseTime(weapon.useTime, player, weapon);
                bool isLastShot = player.itemAnimation <= useTime || // not enough time to shoot again
                    weapon.useLimitPerAnimation != null && player.ItemUsesThisAnimation == weapon.useLimitPerAnimation - 1; // this shot hits the limit

                if (!isLastShot)
                {
                    return true;
                }
            }
            return false;
        }

        //Replicating the exact ammo reservation formula, taken from Player.IsAmmoFreeThisShot as of v2026.3.3.0
        //Removing the checks for first/last shots (handled with IsAmmoFreeFromAnimation instead)
        public static bool ReplicateAmmoSaveFormula(Player player, Item weapon, Item ammo)
        {

            bool flag2 = false;
            if (weapon.type == 3475 && Main.rand.Next(3) != 0)
                flag2 = true;

            if (weapon.type == 3930 && Main.rand.Next(2) == 0)
                flag2 = true;

            if (weapon.type == 3540 && Main.rand.Next(3) != 0)
                flag2 = true;

            if (weapon.type == 5134 && Main.rand.Next(3) == 0)
                flag2 = true;

            /*
            if (magicQuiver && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake) && Main.rand.Next(5) == 0)
            */
            if (player.magicQuiver && AmmoID.Sets.IsArrow[weapon.useAmmo] && Main.rand.Next(5) == 0)
                flag2 = true;

            if (player.ammoBox && Main.rand.Next(5) == 0)
                flag2 = true;

            if (player.ammoPotion && Main.rand.Next(5) == 0)
                flag2 = true;

            if (weapon.type == 1782 && Main.rand.Next(3) == 0)
                flag2 = true;

            if (weapon.type == 98 && Main.rand.Next(3) == 0)
                flag2 = true;

            if (weapon.type == 2270 && Main.rand.Next(2) == 0)
                flag2 = true;

            if (weapon.type == 533 && Main.rand.Next(2) == 0)
                flag2 = true;

            if (weapon.type == 1929 && Main.rand.Next(2) == 0)
                flag2 = true;

            if (weapon.type == 1553 && Main.rand.Next(3) != 0)
                flag2 = true;


            //TML: Clockwork Assault Rifle + Eventide. Handled by consumeAmmoOnLastShotOnly.
            /*
            if (sItem.type == 434 && !ItemAnimationJustStarted)
                flag2 = true;

            if (sItem.type == 4953 && itemAnimation > sItem.useAnimation - 8)
                flag2 = true;
            */

            if (player.huntressAmmoCost90 && Main.rand.Next(10) == 0)
                flag2 = true;

            if (player.chloroAmmoCost80 && Main.rand.Next(5) == 0)
                flag2 = true;

            if (player.ammoCost80 && Main.rand.Next(5) == 0)
                flag2 = true;

            if (player.ammoCost75 && Main.rand.Next(4) == 0)
                flag2 = true;

            // Copied as-is from 1.3
            if (ammo.CountsAsClass(DamageClass.Throwing))
            {
                if (player.ThrownCost50 && Main.rand.Next(100) < 50)
                    flag2 = true;

                if (player.ThrownCost33 && Main.rand.Next(100) < 33)
                    flag2 = true;
            }

            if (Main.remixWorld && weapon.type == 1319 && Main.rand.Next(2) == 0)
                flag2 = true;

            return flag2;
        }
    }
}
