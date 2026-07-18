using Microsoft.Xna.Framework;
using ShinyRemix.Common.ModPlayers;
using ShinyRemix.OOAChanges.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.GlobalProjectiles
{
    public class OctopodProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        Player player;
        float speedMultiplier = 1f;
        float origScale;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.MonkStaffT1;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            player = Main.player[projectile.owner];
            float meleeSpeed = MathHelper.Lerp(player.GetAttackSpeed(DamageClass.Melee), 1f, 0.25f);    //75% of melee speed.
            speedMultiplier = meleeSpeed * ShinyUtils.GetPrefixSpeedModifier(player.HeldItem.prefix); projectile.scale *= player.HeldItem.scale;
            origScale = projectile.scale;
            projectile.scale *= player.HeldItem.scale;
            if (player.meleeScaleGlove)
                projectile.scale += 0.2f;
            else if (ShinyUtils.TRAE && player.GetModPlayer<ShinyMeleeScale>().meleeScaleGlove)
                projectile.scale += 0.2f;

            projectile.localNPCHitCooldown = (int)Math.Ceiling(projectile.localNPCHitCooldown / speedMultiplier);
            base.OnSpawn(projectile, source);
        }
        bool midCheck = false;
        bool lastCheck = false;
        public override bool PreAI(Projectile projectile)
        {
            if(player.whoAmI == Main.myPlayer && projectile.ai[0] > 0f)
            {
                float aiAddition = (speedMultiplier - 1f);
                projectile.ai[0] += aiAddition;

                if (projectile.ai[0] + 1f > 24f && !midCheck)
                {
                    projectile.ai[0] = 24f;
                    midCheck = true;
                }

                if (projectile.ai[0] + 1f > 46f && !lastCheck)
                {
                    projectile.ai[0] = 46f;
                    lastCheck = true;
                }

                float rotPerFrame = 6.2831855f * 2f / 50f * (float)projectile.velocity.X;
                float additionalRot = rotPerFrame * (speedMultiplier - 1f);

                projectile.rotation += additionalRot;

                //Main.NewText($"Sync AIs: {projectile.ai[0]}, {projectile.ai[1]}, {projectile.ai[2]}");
                //Main.NewText($"LocalAIs: {projectile.localAI[0]}, {projectile.localAI[1]}, {projectile.localAI[2]}");

            }
            return base.PreAI(projectile);
        }
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if(projectile.owner == Main.myPlayer)
            {
                player.GetModPlayer<OctopodReuseDelay>().trueReuseDelay = 10f / speedMultiplier;
            }
        }

        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {
            float f2 = projectile.rotation - 0.7853982f * (float)Math.Sign(projectile.velocity.X);
            float collisionPoint7 = 0f;
            float num20 = 65f * (projectile.scale / 1.35f);
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + f2.ToRotationVector2() * (0f - num20), projectile.Center + f2.ToRotationVector2() * num20, 23f * projectile.scale, ref collisionPoint7);
        }
    }
}
