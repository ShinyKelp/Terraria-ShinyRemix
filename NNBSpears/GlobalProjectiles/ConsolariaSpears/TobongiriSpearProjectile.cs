using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class TobongiriSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 238f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "TonbogiriSpear";
        protected override bool UsesCustomHitCooldown => true;
        protected override float HitboxSizeScale => 0.5f;

        protected override bool HasCustomShockwaveEffect => true;

        protected override float ExtensionMultiplier => 7.8f;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            projectile.scale += 0.1f;
        }
        public override bool PreAI(Projectile projectile)
        {
            base.PreAI(projectile);
            return false;
        }

        //Something weird's going on with the Tongogiri, I just do a basic render myself.
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {

            SpriteEffects effects = SpriteEffects.None;
            if(projectile.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }

            Texture2D modTex = TextureAssets.Projectile[projectile.type].Value;
            float modWidth = modTex.Width;

            Vector2 origin = new Vector2(modTex.Width / 2f, modTex.Height / 2f);

            float offset = 96f;
            Vector2 drawPos = projectile.Center - projectile.velocity.SafeNormalize(Vector2.UnitX) * offset - Main.screenPosition;
            
            Main.EntitySpriteDraw(modTex, drawPos, null, lightColor, projectile.rotation, origin, projectile.scale, effects, 0);
            return false;
        }

        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            //Drawing the red glow effect
            Texture2D glow = (Texture2D)ModContent.Request<Texture2D>("Consolaria/Assets/Textures/Projectiles/LightTrail_1");
            SpriteBatch spriteBatch = Main.spriteBatch;
            SpriteEffects spriteEffects = player.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            int totalAnimFrames = (int)projectile.localAI[0];
            bool isThrusting = (player.itemAnimation >= totalAnimFrames * (1f-InitialThrustDuration));

            Vector2 glowOrigin = new Vector2(glow.Width / 2, glow.Height / 2);
            float glowRotation = projectile.rotation;
            if (glowRotation > Math.PI * 2f) glowRotation -= (float)Math.PI * 2f;
            for (int k = 0; k < projectile.oldPos.Length - 1; k++)
            {
                Vector2 glowPosition = new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                glowPosition += projectile.velocity * projectile.scale * projectile.scale * projectile.scale * 32f;
                glowRotation = (float)Math.Atan2(projectile.oldPos[k].Y - projectile.oldPos[k + 1].Y, projectile.oldPos[k].X - projectile.oldPos[k + 1].X);
                float idleFactor = isThrusting? 1.1f : 0.7f;
                if (Vector2.Distance(projectile.oldPos[k], projectile.oldPos[k + 1]) < 0.01f)
                    glowRotation = projectile.velocity.ToRotation();

                Color glowColor = new Color(220 - k * 30, 50, 50 + k * 30, 20);
                spriteBatch.Draw(glow, projectile.oldPos[k] + glowPosition, null, glowColor, glowRotation, glowOrigin, idleFactor * projectile.scale - k / (float)projectile.oldPos.Length, spriteEffects, 0f);
                spriteBatch.Draw(glow, projectile.oldPos[k] * 0.5f + projectile.oldPos[k + 1] * 0.5f + glowPosition, null, glowColor, glowRotation, glowOrigin, idleFactor * projectile.scale - k / (float)projectile.oldPos.Length, spriteEffects, 0f);
            }

            //EXTRA: Drawing shockwave effect similar to Dark Lance / Gungnir
            //Rough adaptation from vanilla code
            //TO-DO: Make this visual functionality generic to give shockwave to any spear?
            Texture2D tearTex = TextureAssets.Extra[Terraria.ID.ExtrasID.SharpTears].Value;
            Vector2 start = player.MountedCenter;
            Vector2 end = projectile.Center;
            float tearProgress = Utils.Remap(player.itemAnimation, player.itemAnimationMax, player.itemAnimationMax / 3f, 0f, 1f, true);
            float intensity = MathF.Sin(tearProgress * MathF.PI);
            float rotation = projectile.velocity.RotatedBy(MathHelper.PiOver2).ToRotation();
            Color color = new Color(180, 50, 90, 20);
            Vector2 baseOffset = new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY;
            Vector2 extensionOffset = projectile.velocity * projectile.scale * 120f;

            int totalDraws = 8;

            for(int i = 0; i < totalDraws; ++i)
            {
                float drawFactor = (float)i / (float)totalDraws;
                Main.EntitySpriteDraw(tearTex, projectile.position + baseOffset + extensionOffset * MathHelper.Lerp(drawFactor, 1f, 0.65f) - Main.screenPosition, null, color * intensity * (1f-drawFactor) * 0.5f, rotation, tearTex.Size() * 0.5f,
                    new Vector2(intensity * 1f, 4.5f * (1f-drawFactor)), SpriteEffects.None, 0);
            }
        }
    }
}
