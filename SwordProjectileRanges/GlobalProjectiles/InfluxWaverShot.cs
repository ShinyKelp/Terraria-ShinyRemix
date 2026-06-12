using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileRanges.GlobalProjectiles
{
    public class InfluxWaverShot : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileRanges && entity.type == ProjectileID.InfluxWaver;
        }

        Player player;
        bool fixSpeed = false;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            player = Main.player[projectile.owner];
            projectile.scale = player.HeldItem.scale;
            if (player.meleeScaleGlove)
                projectile.scale += 0.1f;
            projectile.velocity.Normalize();
            projectile.velocity *= 34f;
            projectile.timeLeft = 23;
            projectile.alpha = 100;
            projectile.damage = (int)(projectile.damage * 0.8f);
        }

        public override bool PreAI(Projectile projectile)
        {
            //Main.NewText($"AI: {projectile.ai[0]}, {projectile.ai[1]}, {projectile.ai[2]}, {projectile.localAI[0]}, {projectile.localAI[1]}, {projectile.localAI[2]},");
            if(projectile.timeLeft == 1 && projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
                projectile.velocity.Normalize();
                projectile.velocity *= 14f;
                projectile.timeLeft += 30;
                fixSpeed = true;
            }
            return base.PreAI(projectile);
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.ai[0] != 0f && !fixSpeed)
            {
                projectile.velocity.Normalize();
                projectile.velocity *= 12.2f;
                projectile.timeLeft += 90;
                fixSpeed = false;
            }
        }
    }
}
