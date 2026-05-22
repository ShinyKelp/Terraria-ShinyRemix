using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class GungnirProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.Gungnir;
        protected override float HoldoutRangeMax => 204f;
        protected override float HoldoutRangeMin => 36f;
        protected override bool HasDustParticles => true;
        protected override bool HasShockwaveEffect => true;
        protected override float ExtensionMultiplier => 2.3f;
        protected override void CreateDustParticles(Projectile projectile)
        {
            Vector2 position = projectile.position;

            Player player = Main.player[projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

            if(duration - projectile.timeLeft > duration * .25f)
            {
                position = projectile.position - projectile.velocity*30;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num19 = Dust.NewDust(position, projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 200, default(Color), 1.2f);
                Main.dust[num19].velocity += projectile.velocity * 0.3f;
                Main.dust[num19].velocity *= 0.2f;
            }

            if (Main.rand.Next(4) == 0)
            {
                int num20 = Dust.NewDust(position, projectile.width, projectile.height, 43, 0f, 0f, 254, default(Color), 0.3f);
                Main.dust[num20].velocity += projectile.velocity * 0.5f;
                Main.dust[num20].velocity *= 0.5f;
            }
        }
    }
}
