using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class TheRottedForkProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.TheRottedFork;
        protected override float HoldoutRangeMax => 128f;
        protected override float HoldoutRangeMin => 16f;
        protected override bool HasDustParticles => true;
        protected override bool HasShockwaveEffect => true;
        protected override float ExtensionMultiplier => 2.9f;
        protected override void CreateDustParticles(Projectile projectile)
        {
            Vector2 position = projectile.position;

            Player player = Main.player[projectile.owner];
            int duration = player.itemAnimationMax; 

            if (duration - projectile.timeLeft > duration * .25f)
            {
                position = projectile.position - projectile.velocity * 18f;
            }

            //Copied from vanilla
            int num21 = Dust.NewDust(position - projectile.velocity * 3f, projectile.width, projectile.height, 115, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 140);
            Main.dust[num21].noGravity = true;
            Main.dust[num21].fadeIn = 1.25f;
            Main.dust[num21].velocity *= 0.25f;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Bleeding, 600);
            base.OnHitNPC(projectile, target, hit, damageDone);

        }
    }
}
