using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.TRAESpears
{
    public class ScourgeSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 142f;
        protected override float HoldoutRangeMin => 40f;
        protected override string ModSpearName => "SoTC";
        protected override bool ShootsProjectile => ShinyOptions.BiomeKeyWeapons;
        protected override int ShotProjectileID => ProjectileID.EatersBite;
        protected override float ShotProjectileSpeed => 13f;
        protected override float ShotProjectileScale => 0.9f;
        protected override float ShotProjectileAt => 0.85f;
        protected override bool ForceManualCollisionDetection => true;

        bool appliedScale = false;

        public override bool PreAI(Projectile projectile)
        {
            if (!appliedScale)
            {
                projectile.scale += 0.1f;
                appliedScale = true;
            }
            return base.PreAI(projectile);
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.spriteDirection == -1)
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            return base.PreDraw(projectile, ref lightColor);
        }
    }
}
