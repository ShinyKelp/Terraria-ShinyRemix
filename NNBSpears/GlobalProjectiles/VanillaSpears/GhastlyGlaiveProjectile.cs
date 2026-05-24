using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using System;
using System.Numerics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class GhastlyGlaiveProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.MonkStaffT2;
        protected override float HoldoutRangeMax => 175f;
        protected override float HoldoutRangeMin => 12f;
        protected override float HoldPositionRelative => 0.78f;
        protected override bool HasDustParticles => true;

        protected override bool UsesCustomHitCooldown => true;


        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            projectile.scale += 0.1f;
        }
        protected override void CreateDustParticles(Projectile projectile)
        {
            //Copied from vanilla
            float num6 = 2f;
            int i = 0;
            Player player = Main.player[projectile.owner];
            projectile.direction = player.direction;
            player.heldProj = projectile.whoAmI;
            Vector2 vector2 = projectile.Center;

            float num = (float)player.itemAnimation / (float)player.itemAnimationMax;
            float num2 = 1f - num;
            float num3 = projectile.velocity.ToRotation();
            float num4 = projectile.velocity.Length();
            float num5 = 22f;
            Vector2 spinningpoint = new Vector2(1f, 0f).RotatedBy((double)(3.1415927f + num2 * 6.2831855f), default(Vector2)) * new Vector2(num4, projectile.ai[0]);
            Vector2 target = vector2 + spinningpoint.RotatedBy((double)num3, default(Vector2)) + new Vector2(num4 + num5 + 40f, 0f).RotatedBy((double)num3, default(Vector2));
            vector2.DirectionTo(projectile.Center);
            Vector2 vector3 = vector2.DirectionTo(target);
            Vector2 vector4 = projectile.velocity.SafeNormalize(Vector2.UnitY);

            Vector2 dustPos = projectile.Center - projectile.velocity * HoldoutRangeMax * 0.12f * projectile.scale;

            while (i < num6)
            {
                Dust dust = Dust.NewDustDirect(dustPos, 14, 14, 228, 0f, 0f, 110, default(Color), 1f);
                dust.velocity = vector2.DirectionTo(dust.position) * 2f;
                dust.position = dustPos + vector4.RotatedBy((double)(num2 * 6.2831855f * 2f + i / num6 * 6.2831855f), default(Vector2)) * 14f * projectile.scale;
                dust.scale = 1f + 0.6f * Main.rand.NextFloat() * projectile.scale;
                dust.velocity += vector4 * 2f;
                dust.noGravity = true;
                i++;
            }
            for (int j = 0; j < 1; j++)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Dust dust2 = Dust.NewDustDirect(dustPos, 20, 20, 228, 0f, 0f, 110, default(Color), 1f);
                    dust2.velocity = vector2.DirectionTo(dust2.position) * 2f;
                    dust2.position = dustPos + vector3 * -110f;
                    dust2.scale = 0.45f + 0.4f * Main.rand.NextFloat() * projectile.scale;
                    dust2.fadeIn = 0.7f + 0.4f * Main.rand.NextFloat();
                    dust2.noGravity = true;
                    dust2.noLight = true;
                }
            }
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            projectile.alpha = 0;

            SpriteEffects effectsDir = 0;
            float quotientAngle = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;

            //quotientAngle -= MathHelper.PiOver2;
            
            Asset<Texture2D> projectileSprite = TextureAssets.Projectile[projectile.type];
            Player player = Main.player[projectile.owner];

            Rectangle frameRect = projectileSprite.Frame(1, 1, 0, 0, 0, 0);
            Rectangle rect = projectile.getRect();
            Vector2 drawVector = Vector2.Zero;
            if (player.direction > 0)
            {
                effectsDir = (SpriteEffects)1;
                drawVector.X = projectileSprite.Width();
                quotientAngle -= 1.5707964f;
            }
            if (player.gravDir == -1f)
            {
                if (projectile.direction == 1)
                {
                    effectsDir = (SpriteEffects)3;
                    drawVector = new Vector2((float)projectileSprite.Width(), (float)projectileSprite.Height());
                    quotientAngle -= 1.5707964f;
                }
                else if (projectile.direction == -1)
                {
                    effectsDir = (SpriteEffects)2;
                    drawVector = new Vector2((float)projectileSprite.Width(), (float)projectileSprite.Height());
                    quotientAngle += 1.5707964f;
                }
            }

            if (effectsDir == SpriteEffects.FlipHorizontally)
                effectsDir = SpriteEffects.None;
            else effectsDir = SpriteEffects.FlipHorizontally;


            Vector2.Lerp(drawVector, frameRect.Center.ToVector2(), 0.25f);
            float num2 = 0f;
            Vector2 trueCenter = projectile.Center + new Vector2(0f, projectile.gfxOffY);
            
            Main.EntitySpriteDraw(projectileSprite.Value, trueCenter - Main.screenPosition, new Rectangle?(frameRect), projectile.GetAlpha(lightColor), quotientAngle, drawVector, projectile.scale, effectsDir, 0f);
            rect.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rect, Color.White * num2);

            return false;
        }

        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {
            return properSpearHitbox.Intersects(targetHitbox);
        }
    }
}
