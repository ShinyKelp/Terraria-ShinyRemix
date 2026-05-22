using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class MythrilHalberdProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.MythrilHalberd;
        protected override float HoldoutRangeMax => 172f;
        protected override float HoldoutRangeMin => 24f;
    }
}