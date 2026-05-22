using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class SwordfishProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.Swordfish;
        protected override float HoldoutRangeMax => 98f;
    }
}