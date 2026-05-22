using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.TRAESpears
{
    public class BoneJavelinProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 76f;
        protected override float HoldoutRangeMin => 8f;
        protected override string ModSpearName => "BoneSpear";
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.spriteDirection == -1)
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            return base.PreDraw(projectile, ref lightColor);
        }

    }
}
