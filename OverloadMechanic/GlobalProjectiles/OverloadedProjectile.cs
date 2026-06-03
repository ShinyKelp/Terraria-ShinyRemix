using ShinyRemix.OverloadMechanic.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace ShinyRemix.OverloadMechanic.GlobalProjectiles
{
    public class OverloadedProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OverloadMechanic && entity.DamageType == DamageClass.Ranged;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            if(source is EntitySource_ItemUse_WithAmmo ammoSource)
            {
                if(ammoSource.Item.TryGetGlobalItem<OverloadedItem>(out OverloadedItem overIt) && overIt.overloaded)
                {
                    Item ammo = player.ChooseAmmo(ammoSource.Item);
                    if(ammo.type == ammoSource.AmmoItemIdUsed && player.CountItem(ammoSource.AmmoItemIdUsed) > 10)
                    {
                        if (OverloadUtils.ReplicateAmmoSaveFormula(player, ammoSource.Item, ammo))
                        {
                            player.ConsumeItem(ammoSource.AmmoItemIdUsed);
                            if (ammo.damage > 30)
                                player.ConsumeItem(ammoSource.AmmoItemIdUsed);
                            
                            //Damage increase: +10% of weapon, +80% of ammo.
                            if(ammo.damage < 30)    
                                projectile.damage += (int)Math.Floor(0.75f * ammo.damage + 0.1f * ammoSource.Item.damage);
                            else
                                projectile.damage += (int)Math.Floor(0.5f * ammo.damage + 0.1f * ammoSource.Item.damage);
                        }
                    }
                    overIt.lastShotFrames = 0;
                }
            }
            base.OnSpawn(projectile, source);
        }
    }
}
