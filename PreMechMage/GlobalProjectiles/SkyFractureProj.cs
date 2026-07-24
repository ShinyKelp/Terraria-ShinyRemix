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

namespace ShinyRemix.PreMechMage.GlobalProjectiles
{
    public class SkyFractureProj : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PreMechMage && entity.type == ProjectileID.SkyFracture;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = 3;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.localAI[0] = 2;
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (projectile.localAI[0] > 0)
            {
                projectile.localAI[0]--;
                if (projectile.velocity.X != oldVelocity.X)
                    projectile.velocity.X = -oldVelocity.X;

                if (projectile.velocity.Y != oldVelocity.Y)
                    projectile.velocity.Y = -oldVelocity.Y;
                CreateDustCircle(projectile);
                return false;
            }
            return true;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            projectile.ai[0] = 1;
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.ai[0] == 1)
            {
                projectile.ai[1]++;
                projectile.velocity *= 0.85f;
                if (projectile.ai[1] >= 40)
                    projectile.Kill();
            }
            return true;
        }

        //Taken from vanilla
        private void CreateDustCircle(Projectile projectile)
        {
            float num110 = 16f;
            int num111 = 0;
            while (num111 < num110)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy((double)(num111 * (6.2831855f / num110)), default(Vector2)) * new Vector2(1f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                int num112 = Dust.NewDust(projectile.Center, 0, 0, 180, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num112].scale = 1.5f;
                Main.dust[num112].noGravity = true;
                Main.dust[num112].position = projectile.Center + spinningpoint5;
                Main.dust[num112].velocity = projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
                num111++;
            }
        }
    }
}
