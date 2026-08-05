using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.MartianBuffs.GlobalProjectiles
{
    public class ChargedBlasterLaser : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return true && entity.type == ProjectileID.ChargedBlasterLaser;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            //projectile.extraUpdates = 1;
        }
        public override bool PreAI(Projectile projectile)
        {
            Main.NewText($"Time left: {projectile.timeLeft}");
            Main.NewText($"Sync AIs: {projectile.ai[0]}, {projectile.ai[1]}, {projectile.ai[2]}");
            Main.NewText($"LocalAIs: {projectile.localAI[0]}, {projectile.localAI[1]}, {projectile.localAI[2]}");
            if (projectile.ai[0] == 200f)
                projectile.ai[0] = 100f;
            return base.PreAI(projectile);
        }
    }
}
