using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class TurtleSpearProjectile2 : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 328f;
        protected override float HoldoutRangeMin => 38f;
        protected override float HoldPositionRelative => 0.75f;
        protected override string ModSpearName => "TurtleSpearProj2";
        protected override float HitboxSizeScale => 0.75f;
    }
}
