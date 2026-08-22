using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinyRemix.Common.GlobalItems;
using ShinyRemix.Common.ModPlayers;
using ShinyRemix.Flails.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Flails.GlobalProjectiles
{
    public class BaseFlailProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        protected Player player;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.FlailChanges && entity.aiStyle == ProjAIStyleID.Flail;
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
            else if (ShinyUtils.TRAE && player.GetModPlayer<ShinyMeleeScale>().meleeScaleGlove)
                projectile.scale += 0.2f;

            if (projectile.owner == Main.myPlayer)
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

        private bool megaLaunch = false;
        private bool megaLaunched = false;
        private bool crashPrevention = false;
        private bool crashing = false;

        /*  AI[0]:
         *  0: Spinning
         *  1: Launching forward
         *  5: Launching forward, hit a tile
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
        public override bool PreAI(Projectile projectile)
        {

            if (projectile.ai[0] == 2f || projectile.ai[0] == 5f)
            {
                //Flails have a weird AF bug: they disappear if they are launched very far, for NO REASON. Only solution I've found
                //is to manually tweak their speed and position so they never get further than 900 distance away from the player.
                if(!crashPrevention && (player.velocity + player.Center).Distance(projectile.velocity + projectile.Center) > 850f)
                {
                    float dist = player.Center.Distance(projectile.Center);
                    if(projectile.velocity.Length() > 20f)
                    {
                        projectile.velocity *= (20f / projectile.velocity.Length());
                    }
                    Vector2 moveDir = projectile.Center.DirectionTo(player.Center);
                    projectile.position += moveDir * (dist - 850f);
                    projectile.velocity = moveDir * projectile.velocity.Length();
                    
                    crashPrevention = true;
                }
            }
            //Predict the frame that flail will be thrown
            else if(projectile.ai[0] == 0f && !player.channel && megaLaunch)
            {
                //Bomb sound ID? Or harpoon?
                SoundStyle style = SoundID.Item1 with
                {
                    Pitch = -0.7f,
                    Volume = 4f
                };
                SoundEngine.PlaySound(style, player.position);
            }

            return true;
        }
        public override void AI(Projectile projectile)
        {

            if (projectile.ai[0] == 0)
            {
                if (baseHitCooldown == 0)
                    baseHitCooldown = projectile.localNPCHitCooldown;

                //Timer that affects swing animation speed
                projectile.localAI[1] += (FinalSpeedModifier * 0.5f);

                if (projectile.localAI[1] >= 60f && !megaLaunch)
                {
                    if(player.whoAmI == Main.myPlayer)
                        ChargeReadyEffects(player);
                    megaLaunch = true;
                }

                //Make flail swing further away from the player
                Vector2 offset = projectile.Center - player.MountedCenter;
                float radiusModifier = projectile.scale / origScale + (player.GetAttackSpeed(DamageClass.Melee)-1f) * 0.25f;
                offset = offset * (radiusModifier - 1f) * 2f;
                projectile.Center += offset;

                float currentRadius = offset.Length();
                projectile.localNPCHitCooldown = (int)Math.Max(Math.Round((float)baseFramesPerSwing / (1f + FinalSpeedModifier * 0.5f)), 2) - 1;
                //projectile.Center = player.MountedCenter + Vector2.Normalize(offset) * currentRadius * radiusModifier;
            }
            else if (projectile.ai[0] == 1 && megaLaunch)
            {
                megaLaunch = false;
                megaLaunched = true;
                projectile.velocity *= 1.5f;
                
                projectile.damage = (int)Math.Floor(projectile.damage * 2.5f);
            }
            else if (projectile.ai[0] != 1 && megaLaunched)
            {
                megaLaunched = false;
                projectile.damage = (int)Math.Ceiling(projectile.damage / 2.5f);
            }

            
            else if (projectile.ai[0] == 6)
            {
                projectile.localNPCHitCooldown = (int)Math.Ceiling(12f / (1f + FinalSpeedModifier));
                projectile.rotation = baseDroppedRotationSpeed * droppedFrameCount * (1f + FinalSpeedModifier * 0.5f) * player.direction;
                droppedFrameCount++;
            }
            crashing = projectile.Distance(player.Center) > 900f;
            //Main.NewText($"Local frames: {projectile.localNPCHitCooldown}");

        }


        void ChargeReadyEffects(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                for (int i = 0; i < 5; i++)
                {
                    int num3 = Dust.NewDust(player.position, player.width, player.height, 45, 0f, 0f, 255, default(Color), (float)Main.rand.Next(20, 26) * 0.1f);
                    Main.dust[num3].noLight = true;
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].velocity *= 0.5f;
                }
            }
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
