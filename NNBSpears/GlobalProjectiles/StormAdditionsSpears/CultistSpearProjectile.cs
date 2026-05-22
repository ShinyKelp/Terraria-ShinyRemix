using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class CultistSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 204f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "CultistSpearProj";

        //To-Do: Override AI completely and port over behaviour in appropiate NNB functions (shoot projectiles, dust particles, etc)
        //Current implementation results in a mix of both spear behaviours
        public override bool PreAI(Projectile projectile)
        {
            base.PreAI(projectile);
            return true;
        }
    }
}
