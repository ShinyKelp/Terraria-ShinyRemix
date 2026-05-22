using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.TRAESpears
{
    public class DaybreakProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 150f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "Daybreak";

        bool appliedScale = false;

        public override bool PreAI(Projectile projectile)
        {
            if (!appliedScale)
            {
                projectile.scale += 0.25f;
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
