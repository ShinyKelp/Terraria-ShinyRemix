using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class TridentProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.Trident;
        protected override float HoldoutRangeMax => 106f;
        protected override float HoldoutRangeMin => 16f;

    }
}
