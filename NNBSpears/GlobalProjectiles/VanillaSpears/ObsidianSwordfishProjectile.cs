using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    //Will have to be updated with 1.4.5 to add fire projectiles.
    public class ObsidianSwordfishProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.ObsidianSwordfish;
        protected override float HoldoutRangeMax => 106f;
        protected override float HoldoutRangeMin => 16f;
    }
}