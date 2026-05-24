using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinyRemix.Flails.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Flails.GlobalProjectiles
{
    public class FlailProjectileBase : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        protected Player player;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.aiStyle == ProjAIStyleID.Flail;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            player = Main.player[projectile.owner];
            origScale = projectile.scale;
            meleeSpeed = player.GetAttackSpeed(DamageClass.Melee);
            weaponSpeed = (float)ContentSamples.ItemsByType[player.HeldItem.type].useAnimation / (float)player.HeldItem.useAnimation;
            float prefixModifier = player.HeldItem.scale / ContentSamples.ItemsByType[player.HeldItem.type].scale;
            projectile.scale *= prefixModifier;
            if (player.meleeScaleGlove)
                projectile.scale += 0.2f;
            
            
            if(projectile.owner == Main.myPlayer)
            {
                Projectile proj = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.position, Vector2.Zero, ModContent.ProjectileType<FlailExtensionProj>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI, origScale);
            }
        }

        const float baseDroppedRotationSpeed = 0.35f;

        const float baseSwingRotationSpeed = 0f;

        const float baseSwingRadiusMultiplier = 1f;

        const float baseFramesPerSwing = 12;

        private int droppedFrameCount = 0;

        private int baseHitCooldown = 0;

        private float origScale = 1f;

        private float meleeSpeed = 1f;
        private float weaponSpeed = 1f;
        private float FinalSpeedModifier => meleeSpeed + weaponSpeed - 2f;

        private float launchGhostGracePeriod = 1f;

        /*  AI[0]:
         *  0: Spinning
         *  1: Launching forward
         *  2: Retreating after launch
         *  6: Dropping
         *  4: Retreating after drop
         *  
         *  AI[1]:
         *  Count of frames launching forward. Reaches either 13 or 15.
         *  
         *  localAI[0]:
         *  Count of tile bounces during launch / frames spent touching a tile in drop
         *  
         *  localAI[1]:
         *  Count of frames spent spinning
         */
        public override void AI(Projectile projectile)
        {

            if (projectile.ai[0] == 0)
            {
                if (baseHitCooldown == 0)
                    baseHitCooldown = projectile.localNPCHitCooldown;

                //Timer that affects swing animation speed
                projectile.localAI[1] += (FinalSpeedModifier * 0.5f);

                //Make flail swing further away from the player
                Vector2 offset = projectile.Center - player.MountedCenter;
                float radiusModifier = projectile.scale / origScale;
                offset = offset * (radiusModifier - 1f) * 2f;
                projectile.Center += offset;

                float currentRadius = offset.Length();
                projectile.localNPCHitCooldown = (int)Math.Max(Math.Round((float)baseFramesPerSwing / (1f + FinalSpeedModifier * 0.5f)), 2) - 1;
                //projectile.Center = player.MountedCenter + Vector2.Normalize(offset) * currentRadius * radiusModifier;
            }
            
            if (projectile.ai[0] == 6)
            {
                projectile.localNPCHitCooldown = (int) Math.Ceiling(12f / (1f + FinalSpeedModifier));
                projectile.rotation = baseDroppedRotationSpeed * droppedFrameCount * (1f + FinalSpeedModifier * 0.5f) * player.direction;
                droppedFrameCount++;
            }

            //Main.NewText($"Local frames: {projectile.localNPCHitCooldown}");

        }

        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {
            float scaleRatio = projectile.scale / origScale;

            int inflateX = (int)((hitbox.Width * scaleRatio - hitbox.Width) / 2f);

            int inflateY = (int)((hitbox.Height * scaleRatio - hitbox.Height) / 2f);

            hitbox.Inflate(inflateX, inflateY);
        }

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (projectile.ai[0] == 0)
                return false;
            return base.CanHitNPC(projectile, target);
        }

        protected void DrawHitbox(Projectile proj)
        {
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            Rectangle hitbox = proj.Hitbox;
            hitbox.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);

            Main.spriteBatch.Draw(pixel, hitbox, Color.Red * 0.5f);
        }
    }
}
