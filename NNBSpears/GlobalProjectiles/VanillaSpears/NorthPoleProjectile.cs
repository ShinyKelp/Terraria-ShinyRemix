using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class NorthPoleProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.NorthPoleWeapon;
        protected override float HoldoutRangeMax => 188f;
        protected override float HoldoutRangeMin => 32f;
        protected override bool ShootsProjectile => true;
        protected override int ShotProjectileID => ProjectileID.NorthPoleSpear;
        protected override float ShotProjectileSpeed => 19f;

        protected override float ShotProjectileAt => 0.8f;
        protected override bool HasDustParticles => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return !ShinyUtils.TRAE && base.AppliesToEntity(entity, lateInstantiation);
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
    }
}