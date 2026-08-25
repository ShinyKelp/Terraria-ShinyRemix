using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Misc.GlobalProjectiles
{
    public class ChargedBlasterLaser : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return true && entity.type == ProjectileID.ChargedBlasterLaser;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            //projectile.extraUpdates = 1;
        }
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.ai[0] == 200f)
                projectile.ai[0] = 100f;
            return base.PreAI(projectile);
        }
    }
}
