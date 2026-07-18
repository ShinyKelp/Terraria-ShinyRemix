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
    public class SkydragonProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        Player player;
        float speedMultiplier = 1f;
        float origScale;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.MonkStaffT3;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            player = Main.player[projectile.owner];
            float meleeSpeed = MathHelper.Lerp(player.GetAttackSpeed(DamageClass.Melee), 1f, 0.25f);    //75% of melee speed.
            speedMultiplier = meleeSpeed * ShinyUtils.GetPrefixSpeedModifier(player.HeldItem.prefix);
            origScale = projectile.scale;
            projectile.scale *= player.HeldItem.scale;
            projectile.scale += 0.1f;
            if (player.meleeScaleGlove)
                projectile.scale += 0.2f;
            else if (ShinyUtils.TRAE && player.GetModPlayer<ShinyMeleeScale>().meleeScaleGlove)
                projectile.scale += 0.2f;

            projectile.localNPCHitCooldown = (int)Math.Ceiling(12f / speedMultiplier);
            base.OnSpawn(projectile, source);
        }
        public override bool PreAI(Projectile projectile)
        {
            if(player.whoAmI == Main.myPlayer && projectile.ai[0] > 0f)
            {
                if (projectile.ai[0] >= 25f)
                    projectile.ai[0] = 0f;
                else
                {
                    float aiAddition = (speedMultiplier - 1f);
                    projectile.ai[0] += aiAddition;
                    if (projectile.ai[0] + 1f > 24f)
                        projectile.ai[0] = 24f;
                }

                float rotPerFrame = 6.2831855f * 2f / 50f * (float)projectile.velocity.X;
                float additionalRot = rotPerFrame * (speedMultiplier - 1f);

                projectile.rotation += additionalRot;
            }
            return base.PreAI(projectile);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.owner == Main.myPlayer)
            {
                Vector2 position = target.Center;
                float width = target.width + 16f;
                float height = target.height + 16f;

                Vector2 randOffset = new Vector2(width * (Main.rand.NextFloat() - 0.5f), height * (Main.rand.NextFloat() - 0.5f));
                Projectile.NewProjectile(projectile.GetSource_FromThis(), position + randOffset, Vector2.Zero, ProjectileID.Electrosphere, (int)Math.Floor(projectile.damage * 0.75f), 0f, projectile.owner, 0f, 0f, 1f);
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {
            float f2 = projectile.rotation - 0.7853982f * (float)Math.Sign(projectile.velocity.X);
            float collisionPoint7 = 0f;
            float num20 = 72f * (projectile.scale / origScale);
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + f2.ToRotationVector2() * (0f - num20), projectile.Center + f2.ToRotationVector2() * num20, 23f * projectile.scale, ref collisionPoint7);
        }
    }
}
