using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class TurtleSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 204f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "TurtleSpearProj";

        protected override float HitboxSizeScale => 1.1f;

        public override bool? CanDamage(Projectile projectile)
        {
            return projectile.timeLeft < player.itemAnimationMax * (1f - (InitialThrustDuration + RetreatDuration));
        }
    }
}
