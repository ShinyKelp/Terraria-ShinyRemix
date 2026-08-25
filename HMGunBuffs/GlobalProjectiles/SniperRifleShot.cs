using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.HMGunBuffs.GlobalProjectiles
{
    public class SniperRifleShot : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.HMGunBuffs && entity.DamageType == DamageClass.Ranged;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo ammoUse && ammoUse.Item.type == ItemID.SniperRifle)
            {
                projectile.extraUpdates++;
                if (projectile.type != ProjectileID.BulletHighVelocity)
                    projectile.extraUpdates += 2;
            }
        }
    }
}
