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

namespace ShinyRemix.BiomeWeapons.GlobalProjectiles
{
    public class RainbowGunProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && (entity.type == ProjectileID.RainbowRodBullet || entity.type == ProjectileID.Flamelash || entity.type == ProjectileID.MagicMissile);
        }
        public bool isGun = false;
        private int initialShotCounter = 15;
        private int rotationPlayerDirection;
        private Player player;
        private Projectile brotherProjectile1, brotherProjectile2;
        const float maxVelocity = 32f;
        const float circleRadius = 32f;
        const float rotationSpeed = 40f;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.player[projectile.owner].HeldItem.type == ItemID.RainbowGun)
            {
                isGun = true;
                player = Main.player[projectile.owner];
                rotationPlayerDirection = player.direction;

                projectile.penetrate = -1;
                projectile.usesIDStaticNPCImmunity = false;
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 8;
                if (projectile.ai[1] == 0f)
                    projectile.ai[0] = 0f;
                else if (projectile.ai[1] == 1f)
                    projectile.ai[0] = rotationSpeed / 3f;
                else if (projectile.ai[1] == 2f)
                    projectile.ai[0] = rotationSpeed / 3f * 2f;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if (!isGun)
                return true;

            if (projectile.type == ProjectileID.Flamelash)
            {
                int frameAux = projectile.frameCounter;
                projectile.frameCounter++;
                if (projectile.frameCounter >= 5)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                    if (projectile.frame >= Main.projFrames[projectile.type])
                    {
                        projectile.frame = 0;
                    }
                }
                FlamelashDust(projectile);
            }
            else if(projectile.type == ProjectileID.MagicMissile)
            {
                MagicMissleDust(projectile);
            }

            if (projectile.ai[2] > 0f && projectile.ai[2] < 1f)
            {
                projectile.velocity.Normalize();
                projectile.velocity = projectile.velocity * maxVelocity * projectile.ai[2];
                projectile.ai[2] += 0.05f;
            }
            if (initialShotCounter > 0)
            {
                initialShotCounter--;
                if (initialShotCounter == 0)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].owner == projectile.owner && i != projectile.whoAmI)
                        {
                            Projectile proj = Main.projectile[i];
                            if (proj.TryGetGlobalProjectile<RainbowGunProj>(out RainbowGunProj rainProj) && rainProj.isGun)
                            {
                                if (brotherProjectile1 == null)
                                    brotherProjectile1 = proj;
                                else
                                    brotherProjectile2 = proj;
                            }
                        }
                    }
                }
                return false;
            }

            if(player.HeldItem.type != ItemID.RainbowGun || !player.channel)
            {
                projectile.Kill();
                return false;
            }

            Vector2 targetPos = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref targetPos);

            projectile.ai[0] += 1f;

            Vector2 rotationDirection = new Vector2(1f, 0f);
            rotationDirection.Normalize();
            rotationDirection = rotationDirection.RotatedBy(MathHelper.TwoPi * rotationPlayerDirection * (projectile.ai[0] % rotationSpeed) / rotationSpeed);
            targetPos += (rotationDirection * circleRadius);

            if (projectile.Distance(targetPos) >= 64f)
            {
                Vector2 v = targetPos - projectile.Center;
                Vector2 vector2 = v.SafeNormalize(Vector2.Zero);
                float num8 = Math.Min((float)maxVelocity, v.Length());
                Vector2 value2 = vector2 * num8;
                if (projectile.velocity.Length() < 4f)
                {
                    projectile.velocity += projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(0.7853981852531433, default(Vector2)).SafeNormalize(Vector2.Zero) * 4f;
                }
                if (projectile.velocity.HasNaNs())
                {
                    projectile.Kill();
                }
                projectile.velocity = Vector2.Lerp(projectile.velocity, value2, 1f);
            }
            else
            {
                projectile.velocity *= 0.3f;
                projectile.velocity += (targetPos - projectile.Center) * 0.3f;
            }

            projectile.rotation = projectile.rotation.AngleLerp(0f, 0.2f);

            return false;
        }

        private void FlamelashDust(Projectile projectile)
        {
            float lerpValue = Utils.GetLerpValue(0f, 10f, 0.8f, true);
            Color newColor = Color.Lerp(Color.Transparent, Color.Crimson, lerpValue);
            if (Main.rand.Next(9) == 0)
            {
                Dust dust6 = Dust.NewDustDirect(projectile.Center, 0, 0, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, newColor, 3.5f);
                dust6.noGravity = true;
                dust6.velocity *= 1.4f;
                dust6.velocity += Main.rand.NextVector2Circular(1f, 1f);
                dust6.velocity += projectile.velocity * 0.15f;
            }
            if (Main.rand.Next(15) == 0)
            {
                Dust dust7 = Dust.NewDustDirect(projectile.Center, 0, 0, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, newColor, 1.5f);
                dust7.velocity += Main.rand.NextVector2Circular(1f, 1f);
                dust7.velocity += projectile.velocity * 0.15f;
            }
        }

        private void MagicMissleDust(Projectile projectile)
        {
            if (Main.rand.Next(9) == 0)
            {
                int num9 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num9].velocity *= 0.3f;
                Main.dust[num9].position.X = projectile.position.X + (float)(projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].position.Y = projectile.position.Y + (float)(projectile.height / 2) + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].noGravity = true;
                Main.dust[num9].velocity += Main.rand.NextVector2Circular(2f, 2f);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!isGun)
                return;
            if(projectile.type == ProjectileID.Flamelash)
            {
                if (target.HasBuff(BuffID.OnFire))
                    target.buffTime[target.FindBuffIndex(BuffID.OnFire)] = 0;
                target.AddBuff(BuffID.OnFire3, 600);
            }
            if (projectile.type == ProjectileID.MagicMissile)
                target.AddBuff(BuffID.Frostburn2, 600);

            int cooldown = projectile.localNPCImmunity[target.whoAmI];

            if (brotherProjectile1 != null && brotherProjectile1.active)
                brotherProjectile1.localNPCImmunity[target.whoAmI] = cooldown;
            if(brotherProjectile2 != null && brotherProjectile2.active)
                brotherProjectile2.localNPCImmunity[target.whoAmI] = cooldown;
        }



        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (brotherProjectile1 != null && brotherProjectile1.active && brotherProjectile1.localNPCImmunity[target.whoAmI] > 0)
                return false;
            if (brotherProjectile2 != null && brotherProjectile2.active && brotherProjectile2.localNPCImmunity[target.whoAmI] > 0)
                return false;
            return base.CanHitNPC(projectile, target);
        }


        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (!isGun)
                return true;
            if (initialShotCounter > 0)
            {
                projectile.Kill();
                return true;

            }
            if (projectile.type == ProjectileID.RainbowRodBullet)
                return true;
            return false;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }

            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }
    }
}
