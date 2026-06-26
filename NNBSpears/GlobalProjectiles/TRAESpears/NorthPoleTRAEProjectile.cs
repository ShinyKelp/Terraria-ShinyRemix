using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ShinyRemix.NNBSpears.GlobalProjectiles.TRAESpears
{
    public class NorthPoleTRAEProjectile : NorthPoleProjectile
    {
        protected override int SpearID => ProjectileID.NorthPoleWeapon;
        protected override float HoldoutRangeMax => 188f;
        protected override float HoldoutRangeMin => 32f;
        protected override bool ShootsProjectile => true;
        protected override bool HasDustParticles => true;
        protected override bool ForceManualCollisionDetection => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SpearRework && ShinyUtils.TRAE && entity.type == SpearID;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 360);
            base.OnHitNPC(projectile, target, hit, damageDone);

        }
        protected override void CreateDustParticles(Projectile projectile)
        {
            Vector2 position = projectile.position;

            Player player = Main.player[projectile.owner];
            int duration = player.itemAnimationMax;

            if (duration - projectile.timeLeft > duration * .25f)
            {
                position = projectile.position - projectile.velocity * 30;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num19 = Dust.NewDust(position, projectile.width, projectile.height, DustID.IceTorch, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 200, default(Color), 1.2f);
                Main.dust[num19].velocity += projectile.velocity * 0.3f;
                Main.dust[num19].velocity *= 0.2f;
            }

            if (Main.rand.Next(4) == 0)
            {
                int num20 = Dust.NewDust(position, projectile.width, projectile.height, DustID.IceTorch, 0f, 0f, 254, default(Color), 0.3f);
                Main.dust[num20].velocity += projectile.velocity * 0.5f;
                Main.dust[num20].velocity *= 0.5f;
            }
        }

        private Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2(MathF.Cos(theta), MathF.Sin(theta)) * radius;
        }
        protected override void ShootProjectiles(Projectile projectile)
        {
            if (!shotProjectile && (float)projectile.timeLeft <= Math.Max(ShotProjectileAt * (float)player.itemAnimationMax, 1f))
            {
                if (ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
                {
                    if (traeMod.TryFind<ModProjectile>("NorthStar", out ModProjectile traeProj))
                    {
                        Vector2 center = properSpearHitbox.Center.ToVector2();
                        bool invertedProj = Main.rand.NextBool();
                        for (int i = 0; i < 5; i++)
                        {
                            float rot = MathF.PI * (i / 4f) - MathF.PI / 2f + projectile.rotation - MathHelper.PiOver4;
                            if (projectile.spriteDirection == 1)
                                rot -= MathHelper.PiOver2;
                            int projID = Projectile.NewProjectile(projectile.GetSource_FromThis(), center, PolarVector(10, rot), traeProj.Type, (int)(projectile.damage * 0.2f), 0f, projectile.owner);
                            
                            Main.projectile[projID].timeLeft += 5-3*Math.Abs(i - 2);
                        }
                    }
                }
                shotProjectile = true;
            }
        }
    }
}