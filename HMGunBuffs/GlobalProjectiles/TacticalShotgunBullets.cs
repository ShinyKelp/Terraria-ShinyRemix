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
    public class TacticalShotgunBullets : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        private int counter = 0;
        private int origPierce = 0;
        private bool valid = false;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.HMGunBuffs && entity.DamageType == DamageClass.Ranged;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_ItemUse_WithAmmo ammoUse && ammoUse.Item.type == ItemID.TacticalShotgun && projectile.type != ProjectileID.ChlorophyteBullet) 
            {
                valid = true;
                origPierce = projectile.penetrate;
                projectile.penetrate = -1;
                projectile.ArmorPenetration += 20;
                if (!projectile.usesIDStaticNPCImmunity)
                {
                    projectile.usesLocalNPCImmunity = true;
                    if (projectile.localNPCHitCooldown != -1)
                        projectile.localNPCHitCooldown = Math.Max(projectile.localNPCHitCooldown, 5);
                }
                else
                {
                    projectile.idStaticNPCHitCooldown = Math.Max(projectile.idStaticNPCHitCooldown, 5);
                }
            }
        }
        public override bool PreAI(Projectile projectile)
        {
            if(!valid) return true;
            counter++;
            if(counter == 20)
            {
                projectile.ArmorPenetration -= 20;
                projectile.penetrate = origPierce;
                valid = false;
            }
                return true;
        }
    }
}
