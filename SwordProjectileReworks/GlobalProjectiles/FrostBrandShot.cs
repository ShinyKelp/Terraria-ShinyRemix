using Microsoft.Xna.Framework;
using ShinyRemix.SwordProjectileReworks.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks.GlobalProjectiles
{
    public class FrostBrandShot : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        private int hitNPC = -1;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileReworks && entity.type == ProjectileID.FrostBoltSword;
        }

        public override void SetDefaults(Projectile entity)
        {
            entity.penetrate = 1;
            entity.usesLocalNPCImmunity = true;
            entity.usesIDStaticNPCImmunity = false;
            entity.localNPCHitCooldown = 10;
        }

        protected virtual float MaxShotAngle => MathHelper.PiOver4;
        protected virtual float RotationStrength => 0.06f;
        protected virtual float RotationOffset => -MathHelper.PiOver4 * 0.75f;
        protected virtual Vector2 BaseShotDistance => new Vector2(-40f, -40f);

        protected virtual int BlizzardDuration => 24;

        protected int initDirection = 1;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = 1;
            projectile.timeLeft = 40;
            //Clamp angle
            Vector2 direction = projectile.velocity.SafeNormalize(Vector2.UnitX);
            if (projectile.velocity.X < 0f)
            {
                direction = -direction;
                initDirection = -1;
            }

            float speed = projectile.velocity.Length();
            float angle = direction.ToRotation();
            angle = MathHelper.Clamp(angle, -MaxShotAngle, MaxShotAngle);

            if (initDirection == -1)
                angle = angle + MathHelper.Pi;
            projectile.velocity = angle.ToRotationVector2().RotatedBy(RotationOffset * initDirection) * speed;

            Player player = Main.player[projectile.owner];

            projectile.position.X += BaseShotDistance.X * player.direction;
            projectile.position.Y += BaseShotDistance.Y;
            base.OnSpawn(projectile, source);
        }

        public override void AI(Projectile projectile)
        {
            float initRotationHalt = 1f - (float)Math.Max(projectile.timeLeft - 25, 0) / 15f;
            projectile.velocity = projectile.velocity.RotatedBy(RotationStrength * initDirection * initRotationHalt);
            base.AI(projectile);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.damage < 30)
                target.AddBuff(BuffID.Frostburn, 420);
            else
                target.AddBuff(BuffID.Frostburn2, 600);

            hitNPC = target.whoAmI;

            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if(projectile.owner == Main.myPlayer && BlizzardDuration > 0)
            {
                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(), 
                    projectile.Center, 
                    Vector2.Zero, 
                    ModContent.ProjectileType<FrostBlastMelee>(), 
                    (int)Math.Round(projectile.damage*0.75f), 
                    projectile.knockBack * 0.5f, 
                    projectile.owner,
                    Math.Sign(projectile.velocity.X),
                    BlizzardDuration,
                    hitNPC);
            }
            base.OnKill(projectile, timeLeft);
        }
    }
}
