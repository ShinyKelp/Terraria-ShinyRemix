using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileRanges.GlobalProjectiles
{
    public class TerraBladeShot : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private Vector2 originalVelocity = Vector2.Zero;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileRanges && entity.type == ProjectileID.TerraBlade2Shot;
        }


        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            originalVelocity = projectile.velocity;

            player = Main.player[projectile.owner];
            originalPlayerStart = player.Hitbox.Center.ToVector2();

            projectile.timeLeft = totalLifetime;
            projectile.tileCollide = false;

            Vector2 direction = projectile.velocity;
            direction.Normalize();
            projectile.position -= direction * 80f;


            base.OnSpawn(projectile, source);
        }

        Player player;
        Vector2 originalPlayerStart;
        const int totalLifetime = 38;
        const int startFadeThreshold = 18;
        const float startDecayThreshold = 0.04f;
        const float speedBase = 0.6f;
        bool collided = false;
        public override void AI(Projectile projectile)
        {
            //Since projectile starts behind the player, collisions are disabled for three frames.
            //On the second frame, we do a manual raycast to check if the player is right against a wall (and the projectile might have already passed it)
            //Note: localAI[1] is a flag that determines when the projectile hits a block and is set to halt and rapidly fade away.
            if (projectile.timeLeft < totalLifetime - 2)
                projectile.tileCollide = true;
            else if (projectile.timeLeft == totalLifetime - 1)
            {
                Vector2 direction = projectile.velocity.SafeNormalize(Vector2.UnitX);

                float maxDistance = 2000f;

                float[] samples = new float[3];

                Collision.LaserScan(
                    originalPlayerStart,
                    direction,
                    0.0f,
                    maxDistance,
                    samples
                );

                float[] samples2 = new float[3];
                Vector2 lowerPlayerStart = originalPlayerStart;
                lowerPlayerStart.Y += 8f;
                Collision.LaserScan(
                    lowerPlayerStart,
                    direction,
                    0.0f,
                    maxDistance,
                    samples2
                );

                float distance1 = samples.Average();
                float distance2 = samples2.Average();

                float distance = Math.Max(distance1, distance2);

                if (distance < 60f)
                {
                    projectile.localAI[1] = 1f;
                    collided = true;
                }
            }
            
            if (projectile.timeLeft == startFadeThreshold)
                projectile.localAI[1] = 1f;

            float progress = 1f - projectile.timeLeft / (float)totalLifetime;

            if (projectile.localAI[1] == 1f || collided)    //If vanilla AI detects a collision (not standard collision detection)
            {
                progress = MathHelper.Lerp(0.4f, 1f, progress);
            }

            float finalSpeedFactor;

            //Fast speed, then exponential decay
            if (progress < startDecayThreshold)
            {
                finalSpeedFactor = speedBase;
            }
            else
            {
                float t = (progress - startDecayThreshold) / (1f - startDecayThreshold);

                float exponent = -6f;
                finalSpeedFactor = MathF.Exp(exponent * t);
                finalSpeedFactor = Math.Max(finalSpeedFactor, 0.04f);
            }
            projectile.velocity = originalVelocity * finalSpeedFactor;
        }

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (collided)
                return false;
            return base.CanHitNPC(projectile, target);
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            collided = true;
            return base.OnTileCollide(projectile, oldVelocity);
        }
    }
}
