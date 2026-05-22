using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class CobaltNaginataProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.CobaltNaginata;
        protected override float HoldoutRangeMax => 152f;
        protected override float HoldoutRangeMin => 24f;
    }
}