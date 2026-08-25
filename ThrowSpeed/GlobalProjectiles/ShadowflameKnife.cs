using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.ThrowSpeed.GlobalProjectiles
{
    public class ShadowflameKnife : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.ThrowSpeedScaling && entity.type == ProjectileID.ShadowFlameKnife;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.ai[0] = 5f;
        }
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.ai[0] == 30f && projectile.penetrate >= 3)
            {
            projectile.damage = (int)Math.Floor(projectile.damage * 0.75f);

            }
            return base.PreAI(projectile);
        }
    }
}
