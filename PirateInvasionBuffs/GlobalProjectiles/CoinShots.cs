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

namespace ShinyRemix.PirateInvasionBuffs.GlobalProjectiles
{
    public class CoinShots : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.CopperCoin || entity.type == ProjectileID.SilverCoin ||
                entity.type == ProjectileID.GoldCoin || entity.type == ProjectileID.PlatinumCoin;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = 2;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.penetrate > 1)
                return true;
            projectile.velocity.X *= 0.98f;
            if(projectile.velocity.Y < 12f)
                projectile.velocity.Y += 0.15f;
            projectile.rotation += MathHelper.PiOver4 * 0.2f * projectile.ai[0] * Math.Sign(projectile.velocity.X);
            return false;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.penetrate != 1)
            {
                float num26 = projectile.velocity.Length();
                Vector2 vector5 = new Vector2(Main.rand.NextFloat()*2f - 1f, Main.rand.NextFloat() + 1.8f);
                projectile.ai[0] = vector5.Y - 0.8f;
                vector5 *= num26;
                projectile.velocity = -vector5 * 0.2f;
                projectile.netUpdate = true;
                projectile.damage = (int)Math.Floor(projectile.damage * 0.75f);
            }
        }
    }
}
