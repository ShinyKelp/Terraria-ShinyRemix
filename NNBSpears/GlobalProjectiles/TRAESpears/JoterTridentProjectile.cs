using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.TRAESpears
{
    public class JoterTridentProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 160f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "JoterTridentSpear";
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.spriteDirection == -1)
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            return base.PreDraw(projectile, ref lightColor);
        }
    }
}
