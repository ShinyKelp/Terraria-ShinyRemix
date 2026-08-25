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
    public class UziMagnumBullets : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.HMGunBuffs && entity.DamageType == DamageClass.Ranged;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_ItemUse_WithAmmo ammoUse && (ammoUse.Item.type == ItemID.Uzi || ammoUse.Item.type == ItemID.VenusMagnum))
            {
                if (projectile.penetrate > 0 && projectile.type != ProjectileID.ChlorophyteBullet)
                {
                    projectile.penetrate++;
                    if (!projectile.usesIDStaticNPCImmunity)
                    {
                        projectile.usesLocalNPCImmunity = true;
                        if(projectile.localNPCHitCooldown != -1)
                            projectile.localNPCHitCooldown = Math.Max(projectile.localNPCHitCooldown, 5);
                    }
                    else
                    {
                        projectile.idStaticNPCHitCooldown = Math.Max(projectile.idStaticNPCHitCooldown, 5);
                    }
                }
                if (projectile.type != ProjectileID.BulletHighVelocity)
                    projectile.extraUpdates++;
            }
        }
    }
}
