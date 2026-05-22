using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class DarkLanceProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.DarkLance;
        protected override float HoldoutRangeMax => 158f;
        protected override float HoldoutRangeMin => 16f;
        protected override float HoldPositionRelative => 0.75f;
        protected override bool HasDustParticles => true;
        protected override bool HasShockwaveEffect => true;

        protected override float ExtensionMultiplier => 2.7f;
        protected override void CreateDustParticles(Projectile projectile)
        {
            Vector2 position = projectile.position;

            Player player = Main.player[projectile.owner]; 
            int duration = player.itemAnimationMax;

            if (duration - projectile.timeLeft > duration * .25f)
            {
                position = projectile.position - projectile.velocity * 20;
            }
            //Copied from vanilla
            if (Main.rand.Next(5) == 0)
                Dust.NewDust(position, projectile.width, projectile.height, 14, 0f, 0f, 150, default(Color), 1.4f);
            int num18 = Dust.NewDust(position, projectile.width, projectile.height, 27, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
            Main.dust[num18].noGravity = true;
            Main.dust[num18].velocity /= 2f;
            num18 = Dust.NewDust(position - projectile.velocity * 2f, projectile.width, projectile.height, 27, 0f, 0f, 150, default(Color), 1.4f);
            Main.dust[num18].velocity /= 5f;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 600);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        
    }
}
