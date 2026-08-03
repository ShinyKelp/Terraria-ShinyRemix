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
    public class PosessedHatchet : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.ThrowSpeedScaling && entity.type == ProjectileID.PossessedHatchet;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.localAI[0] = (float)Math.Floor(projectile.damage * 0.1f);
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.ai[0] == 0f && projectile.ai[1] > 25f && projectile.ai[1]%10 == 0)
            {
                projectile.damage = (int)Math.Max(projectile.localAI[0] * 5f, (float)projectile.damage - projectile.localAI[0]);
            }
            return true;
        }
    }
}
