using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class PalladiumPikeProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.PalladiumPike;
        protected override float HoldoutRangeMax => 156f;
        protected override float HoldoutRangeMin => 24f;
    }
}