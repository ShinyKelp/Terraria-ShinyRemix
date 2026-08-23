using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks.GlobalProjectiles
{
    public class TizonaShot : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileReworks && ShinyUtils.Consolaria && entity.type == SwordProjectileReworkUtils.TizonaProjType;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate += 3;
        }
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.localAI[0] == 16f)
            {
                projectile.velocity *= 0.3f;
            }
            return true;
        }
    }
}
