using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class StormSpearProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.ThunderSpear;
        protected override float HoldoutRangeMax => 100f;
        protected override bool ShootsProjectile => true;
        protected override int ShotProjectileID => ProjectileID.ThunderSpearShot;
        protected override float ShotProjectileSpeed => 16f;
        protected override float ShotProjectileAt => 0.85f;
        protected override bool HasDustParticles => true;

        protected override void CreateDustParticles(Projectile projectile)
        {
            Vector2 position = projectile.position;
            Player player = Main.player[projectile.owner];
            int duration = player.itemAnimationMax; 

            if (duration - projectile.timeLeft > duration * .25f)
            {
                position = projectile.position - projectile.velocity * 16f;
            }

            //Copied from vanilla
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustDirect(position, projectile.width, projectile.height, 226, 0f, 0f, 150, default(Color), 0.7f);
                dust.noGravity = true;
                dust.velocity *= 1.4f;
            }

            if (Main.rand.Next(5) == 0)
                Dust.NewDustDirect(position, projectile.width, projectile.height, 226, 0f, 0f, 150, default(Color), 0.5f).velocity.Y -= 0.5f;

        }
    }
}