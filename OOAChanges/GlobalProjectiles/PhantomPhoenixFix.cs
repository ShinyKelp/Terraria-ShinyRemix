using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.GlobalProjectiles
{
    public class PhantomPhoenixFix : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.DD2PhoenixBow;
        }
        public override void PostAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[1] > (float)player.itemAnimationMax)
                projectile.ai[1] = (float)player.itemAnimationMax;
        }
    }
}
