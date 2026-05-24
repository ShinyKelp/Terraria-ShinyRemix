using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles
{
    public class SpearProjectileBase : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        protected virtual int SpearID => ProjectileID.Spear;
        //Thrusting motion variables
        protected virtual float HoldoutRangeMin => 8f;
        protected virtual float HoldoutRangeMax => 86f;
        protected virtual float HoldPositionRelative => .7f;
        protected virtual float InitialThrustDuration => 0.3f;
        protected virtual float RetreatDuration => 0.15f;
        protected float progress;
        //Dust variables
        protected virtual bool HasDustParticles => false;
        
        //Single projectile variables (applicable for Chlorophyte Partisan, North Pole, etc)
        protected virtual bool ShootsProjectile => false;
        protected virtual int ShotProjectileID => ProjectileID.SporeCloud;
        protected virtual float ShotProjectileAt => 1f-InitialThrustDuration; //1f: Shot immediately. 0f: Shot at the end of projectile lifespan.
        protected virtual float ShotProjectileSpeed => 3f;
        protected bool shotProjectile = false;
        //Shockwave effect (hitbox only)
        protected virtual bool HasShockwaveEffect => false;
        protected virtual float ExtensionMultiplier => 1f;

        //Other
        protected virtual float HitboxSizeScale => 1f;
        protected Player player;

        protected int[] hitCooldown = new int[Main.maxNPCs];

        protected virtual bool UsesCustomHitCooldown => false;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == SpearID;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            player = Main.player[projectile.owner];
            projectile.scale = player.HeldItem.scale;
            if (player.meleeScaleGlove)
                projectile.scale += 0.2f;

            Main.NewText($"Values:\nItemAnimMax: {player.itemAnimationMax}\nItemAnim: {player.itemAnimation}\nUseTime: {player.HeldItem.useTime}\nUseAnim: {player.HeldItem.useAnimation}");
            //Store duration for stable use in AI
            projectile.localAI[0] = player.itemAnimationMax;

            projectile.timeLeft = (int)projectile.localAI[0];

            //Immunity frames scale with item speed, ensuring two hits per thrust but with custom timing.
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = (int)Math.Ceiling(projectile.localAI[0] * 0.5f);
            if(ShinyUtils.TRAE && player.HeldItem != null && player.HeldItem.UseSound == null)
                SoundEngine.PlaySound(SoundID.Item1, projectile.Center);
        }


        //ExampleMod taken as a base for the custom thrusting motion.
        public override bool PreAI(Projectile projectile)
        {
            if (UsesCustomHitCooldown)
            {
                for (int i = 0; i < hitCooldown.Length; i++)
                {
                    if (hitCooldown[i] > 0)
                        hitCooldown[i]--;
                }
            }

            int duration = (int)projectile.localAI[0]; // Define the duration the projectile will exist in frames
            
            player.heldProj = projectile.whoAmI; // Update the player's held projectile id

            //Extra AI nuance: spear follows mouse during full animation.
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 shootingDirection = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);
            projectile.velocity = shootingDirection;
            // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            //Thrusting animation variables.
            float initialThrustDurationAbs = duration * InitialThrustDuration;
            float retreatDurationAbs = duration * RetreatDuration;
            float initialThrustTiming = duration - initialThrustDurationAbs;
            float retreatTiming = duration - initialThrustDurationAbs - retreatDurationAbs;
            float scaledHoldPosRelative = MathHelper.Lerp(HoldPositionRelative, HoldPositionRelative * projectile.scale, 0.1f);
            float retreatDistance = 1 - scaledHoldPosRelative;

            
            if (player.itemAnimation >= initialThrustTiming)
            {
                //Initial thrust animation. Progress goes from 0.0 to 1.0 extremely fast.
                progress = (duration - player.itemAnimation) / initialThrustDurationAbs;
            }
            else if (player.itemAnimation < initialThrustTiming && player.itemAnimation >= retreatTiming)
            {
                //Partial retreat animation. Progress goes from 1.0 to HoldPositionRelative fast.
                progress = (player.itemAnimation - retreatTiming) / retreatDurationAbs;
                progress = progress * retreatDistance + HoldPositionRelative;
            }
            else
            {
                //After partial retreat, spear stays in place for rest of the animation.
                progress = scaledHoldPosRelative;
            }

            float ScaledHoldoutRangeMin = MathHelper.Lerp(HoldoutRangeMin,HoldoutRangeMin * (1f-projectile.scale), 0.7f);
            float ScaledHoldoutRangeMax = MathHelper.Lerp(HoldoutRangeMax, HoldoutRangeMax * projectile.scale, 0.85f);
            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back. NOTE: using Lerp intead of Smoothstep for a sharper motion
            projectile.Center = player.MountedCenter + Vector2.Lerp(projectile.velocity * ScaledHoldoutRangeMin, projectile.velocity * ScaledHoldoutRangeMax, progress);

            //Set projectile rotation values to align with AI calculations. Mostly for mod compatibility.
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
            projectile.spriteDirection = -player.direction;
            if (player.direction < 0)
                projectile.rotation += MathHelper.PiOver2;

            if (ShootsProjectile)
                ShootProjectiles(projectile);

            if (HasDustParticles)
                CreateDustParticles(projectile);

            return false;
        }


        //As of 1.4.4, seven spears shoot projectiles. Six of them shoot exactly one and are generalized under this function.
        //The seventh one: the Mushroom Spear, overrides the function instead.
        protected virtual void ShootProjectiles(Projectile projectile)
        {
            if (!shotProjectile && (float)player.itemAnimation <= Math.Max(ShotProjectileAt * (float)projectile.localAI[0], 1f))
            {
                Projectile proj = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity * ShotProjectileSpeed, ShotProjectileID, projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
                proj.scale = projectile.scale;
                shotProjectile = true;
            }
        }

        //Vanilla spears have many differing dust behaviours. Not possible to create a generic one.
        //Functions in child classes have code copy-pasted from vanilla.
        protected virtual void CreateDustParticles(Projectile projectile)
        {
        }


        //Adjust the hitbox to better align with the spear's tip.
        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {
            //Scale hitbox roughly to spear
            hitbox.Width = (int)(HoldoutRangeMax * 0.21f * projectile.scale * HitboxSizeScale);
            hitbox.Height = (int)(HoldoutRangeMax * 0.21f * projectile.scale * HitboxSizeScale);

            //Hitbox position, calculated the same way as the sprite
            float ScaledHoldoutRangeMin = MathHelper.Lerp(HoldoutRangeMin, HoldoutRangeMin * (1f - projectile.scale), 0.7f) * 0.87f;
            float ScaledHoldoutRangeMax = MathHelper.Lerp(HoldoutRangeMax, HoldoutRangeMax * projectile.scale, 0.85f) * 0.87f;

            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 direction = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);

            Vector2 hitboxPos = player.MountedCenter + Vector2.Lerp(direction * ScaledHoldoutRangeMin, direction * ScaledHoldoutRangeMax, progress);

            hitbox.Location = new((int)hitboxPos.X - hitbox.Width / 2, (int)hitboxPos.Y - hitbox.Height / 2);
            properSpearHitbox = hitbox;
        }

        public Rectangle properSpearHitbox;

        protected void DrawHitbox()
        {
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            Rectangle hitbox = properSpearHitbox;
            hitbox.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);

            Main.spriteBatch.Draw(pixel, hitbox, Color.Red * 0.5f);
        }

        //Vanilla shockwave spears have annoying semi-hardcoded hitbox detection, we have to manually prevent it
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (UsesCustomHitCooldown)
            {
                if (hitCooldown[target.whoAmI] > 0)
                    return false;
            }
            if (!HasShockwaveEffect)
                return base.CanHitNPC(projectile, target);
            else
            {
                if (Colliding(projectile, projectile.Hitbox, target.Hitbox).Value)
                    return null;
                else return false;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(UsesCustomHitCooldown)
                hitCooldown[target.whoAmI] = projectile.localNPCHitCooldown;
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        private bool attemptedShockwave = false;

        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!HasShockwaveEffect)
                return base.Colliding(projectile, projHitbox, targetHitbox);
            else
            {
                Rectangle spearHitbox = properSpearHitbox;
                if (properSpearHitbox.Width == 0)
                    spearHitbox = projHitbox;

                int duration = (int)projectile.localAI[0];
                if (player.itemAnimation < duration * (1-InitialThrustDuration) && attemptedShockwave)
                {
                    return spearHitbox.Intersects(targetHitbox);
                }
                else
                {
                    attemptedShockwave = true;
                    Vector2 maxRangePos = spearHitbox.Center.ToVector2() + projectile.velocity * (spearHitbox.Width * ExtensionMultiplier);
                    float collisionPoint = 0f;
                    if (Collision.CheckAABBvLineCollision(
                        targetHitbox.TopLeft(),
                        targetHitbox.Size(),
                        spearHitbox.Center.ToVector2(),
                        maxRangePos,
                        projHitbox.Width,
                        ref collisionPoint))
                    {
                        return true;
                    }
                    else return false;

                }
            }
        }



        //Code taken and adapted from vanilla for easier study. This function is not utilized.
        private void VanillaDraw(Projectile projectile, ref Color lightColor)
        {
            SpriteEffects effectsDir = 0;
            float quotientAngle = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;
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
            Vector2.Lerp(drawVector, frameRect.Center.ToVector2(), 0.25f);
            float num2 = 0f;
            Vector2 trueCenter = projectile.Center + new Vector2(0f, projectile.gfxOffY);
            Rectangle extensionBox;
            if (projectile.AI_019_Spears_GetExtensionHitbox(out extensionBox))
            {
                Vector2 playerOffsetRotation = player.RotatedRelativePoint(player.MountedCenter, false, false);
                float extentionRatio = extensionBox.Size().Length() / projectile.Hitbox.Size().Length();
                //new Color(255, 255, 255, 0) * 1f;
                float shockwaveAnimProgressInSpear = Utils.Remap((float)player.itemAnimation, (float)player.itemAnimationMax, (float)player.itemAnimationMax / 3f, 0f, 1f, true);
                float shockwaveAnimProgress = Utils.Remap(shockwaveAnimProgressInSpear, 0f, 0.3f, 0f, 1f, true) * Utils.Remap(shockwaveAnimProgressInSpear, 0.3f, 1f, 1f, 0f, true);
                shockwaveAnimProgress = 1f - (1f - shockwaveAnimProgress) * (1f - shockwaveAnimProgress);
                Vector2 offsetExtensionPos = extensionBox.Center.ToVector2() + new Vector2(0f, projectile.gfxOffY);
                Vector2.Lerp(playerOffsetRotation, offsetExtensionPos, 1.1f);
                Texture2D shockwaveTexture = TextureAssets.Extra[ExtrasID.SharpTears].Value;
                Vector2 origin = shockwaveTexture.Size() / 2f;
                Color shockwaveColor = new Color(255, 255, 255, 0) * 0.5f;

                switch (projectile.type)
                {
                    case ProjectileID.Gungnir:
                        shockwaveColor = new Color(255, 220, 80, 0);
                        break;
                    case ProjectileID.DarkLance:
                        shockwaveColor = new Color(180, 80, 255, 0);
                        break;
                    case ProjectileID.NorthPoleWeapon:
                        shockwaveColor = new Color(80, 140, 255, 0);
                        break;
                    case ProjectileID.TheRottedFork:
                        shockwaveColor = new Color(255, 50, 30, 15);
                        break;
                }

                float shockwaveAngle = quotientAngle - 0.7853982f * (float)projectile.spriteDirection;
                if (player.gravDir < 0f)
                {
                    shockwaveAngle -= 1.5707964f * (float)projectile.spriteDirection;
                }
                Main.EntitySpriteDraw(shockwaveTexture, Vector2.Lerp(offsetExtensionPos, trueCenter, 0.5f) - Main.screenPosition, null, shockwaveColor * shockwaveAnimProgress, shockwaveAngle, origin, new Vector2(shockwaveAnimProgress * extentionRatio, extentionRatio) * projectile.scale * extentionRatio, effectsDir, 0f);
                Main.EntitySpriteDraw(shockwaveTexture, Vector2.Lerp(offsetExtensionPos, trueCenter, 1f) - Main.screenPosition, null, shockwaveColor * shockwaveAnimProgress, shockwaveAngle, origin, new Vector2(shockwaveAnimProgress * extentionRatio, extentionRatio * 1.5f) * projectile.scale * extentionRatio, effectsDir, 0f);
                Main.EntitySpriteDraw(shockwaveTexture, Vector2.Lerp(playerOffsetRotation, trueCenter, shockwaveAnimProgressInSpear * 1.5f - 0.5f) - Main.screenPosition + new Vector2(0f, 2f), null, shockwaveColor * shockwaveAnimProgress, shockwaveAngle, origin, new Vector2(shockwaveAnimProgress * extentionRatio * 1f * shockwaveAnimProgress, extentionRatio * 2f * shockwaveAnimProgress) * projectile.scale * extentionRatio, effectsDir, 0f);
                for (float num7 = 0.4f; num7 <= 1f; num7 += 0.1f)
                {
                    Vector2 vector4 = Vector2.Lerp(playerOffsetRotation, offsetExtensionPos, num7 + 0.2f);
                    Main.EntitySpriteDraw(shockwaveTexture, vector4 - Main.screenPosition + new Vector2(0f, 2f), null, shockwaveColor * shockwaveAnimProgress * 0.75f * num7, shockwaveAngle, origin, new Vector2(shockwaveAnimProgress * extentionRatio * 1f * shockwaveAnimProgress, extentionRatio * 2f * shockwaveAnimProgress) * projectile.scale * extentionRatio, effectsDir, 0f);
                }
                extensionBox.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            }
            Main.EntitySpriteDraw(projectileSprite.Value, trueCenter - Main.screenPosition, new Rectangle?(frameRect), projectile.GetAlpha(lightColor), quotientAngle, drawVector, projectile.scale, effectsDir, 0f);
            rect.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rect, Color.White * num2);
        }
    }
}
