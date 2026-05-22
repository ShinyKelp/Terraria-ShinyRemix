using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class BeetleSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 204f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "BeetleSpearProj";
        protected override bool UsesCustomHitCooldown => true;

        private int detectedBrothers = 0;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            for(int i = 0; i < Main.projectile.Length; ++i)
            {
                if (Main.projectile[i] != null && Main.projectile[i].active && Main.projectile[i].type == projectile.type && Main.projectile[i].owner == projectile.owner)
                {
                    detectedBrothers++;
                }
            }
        }

        //To-Do: Override AI completely and port over behaviour in appropiate NNB functions (shoot projectiles, dust particles, etc)
        //Current implementation results in a mix of both spear behaviours
        public override bool PreAI(Projectile projectile)
        {
            base.PreAI(projectile);
            if(projectile.alpha == 100)
            {
                float rotation = MathHelper.ToRadians(20f);
                float rotationDecider = 0f;
                if (detectedBrothers == 2)
                    rotationDecider = 0f;
                else rotationDecider = 1f;
                
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy((double)MathHelper.Lerp(-rotation, rotation, rotationDecider));
                projectile.velocity = perturbedSpeed;
                projectile.rotation = perturbedSpeed.ToRotation();
                
                float ScaledHoldoutRangeMin = MathHelper.Lerp(HoldoutRangeMin, HoldoutRangeMin * (1f - projectile.scale), 0.7f);
                float ScaledHoldoutRangeMax = MathHelper.Lerp(HoldoutRangeMax, HoldoutRangeMax * projectile.scale, 0.85f);
                projectile.Center = player.MountedCenter + Vector2.Lerp(projectile.velocity * ScaledHoldoutRangeMin, projectile.velocity * ScaledHoldoutRangeMax, progress);
            }
            return true;
        }

        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {
            return;
        }
    }
}
