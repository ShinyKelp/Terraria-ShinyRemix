using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.TerraBladeTree.GlobalProjectiles
{
    public class TerraBladeShotDamage : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.TerraBlade2Shot;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.damage = (int)(projectile.damage * 0.75f);
            base.OnSpawn(projectile, source);
        }
    }
}
