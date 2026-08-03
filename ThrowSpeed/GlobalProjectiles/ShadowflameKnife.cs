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
            return true && entity.type == ProjectileID.ShadowFlameKnife;
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
            if (projectile.ai[0] == 30f && projectile.penetrate >= 4)
                projectile.damage = (int)Math.Floor(projectile.damage * 0.75f);
            Main.NewText($"Sync AIs: {projectile.ai[0]}, {projectile.ai[1]}, {projectile.ai[2]}");
            Main.NewText($"LocalAIs: {projectile.localAI[0]}, {projectile.localAI[1]}, {projectile.localAI[2]}");
            return base.PreAI(projectile);
        }
    }
}
