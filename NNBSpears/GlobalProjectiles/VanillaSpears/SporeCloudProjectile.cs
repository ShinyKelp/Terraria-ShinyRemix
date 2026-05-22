using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class SporeCloudProjectile : GlobalProjectile
    {
        private int originalWidth;
        private int originalHeight;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.SporeCloud;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom,1200);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.scale = Main.player[projectile.owner].HeldItem.scale;
            projectile.Hitbox.Inflate((int)(projectile.Hitbox.Width * (projectile.scale-1)), (int)(projectile.Hitbox.Height * (projectile.scale-1)));
            originalWidth = projectile.Hitbox.Width;
            originalHeight = projectile.Hitbox.Height;
            projectile.timeLeft = 100;
        }
        public override bool PreAI(Projectile projectile)
        {
            projectile.scale *= 1.02f;
            float dropoff = 1f - (1f - (1f - projectile.timeLeft / 100f)) * (1f - (1f - projectile.timeLeft / 100f));
            projectile.alpha = (int)(255f * dropoff);
            projectile.velocity *= 0.96f;
            int frameProgress = projectile.frameCounter + 1;
            projectile.frameCounter = frameProgress;
            if (frameProgress >= 6)
            {
                projectile.frameCounter = 0;
                frameProgress = projectile.frame + 1;
                projectile.frame = frameProgress;
                if (frameProgress >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            return false;

        }
        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {
            int extraXValue = (int)Math.Floor((originalWidth * projectile.scale - hitbox.Width)/3.5);
            int extraYValue = (int)Math.Floor((originalHeight * projectile.scale - hitbox.Height)/3.5);

            hitbox.Inflate(extraXValue, extraYValue);
        }
    }
}
