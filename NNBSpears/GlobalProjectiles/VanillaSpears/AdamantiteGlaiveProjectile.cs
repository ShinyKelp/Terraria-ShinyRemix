using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class AdamantiteGlaiveProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.AdamantiteGlaive;
        protected override float HoldoutRangeMax => 202f;
        protected override float HoldoutRangeMin => 32f;
    }
}