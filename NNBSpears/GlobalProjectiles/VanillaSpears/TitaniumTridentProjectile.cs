using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class TitaniumTridentProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.TitaniumTrident;
        protected override float HoldoutRangeMax => 176f;
        protected override float HoldoutRangeMin => 28f;
    }
}