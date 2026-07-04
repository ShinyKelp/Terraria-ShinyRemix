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
            return ShinyOptions.TerraBladeTree && entity.type == ProjectileID.TerraBlade2Shot;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(ShinyOptions.SwordProjectileReworks)
                projectile.damage = (int)(projectile.damage * 0.75f);
            else
                projectile.damage = (int)(projectile.damage * 0.7f);
            base.OnSpawn(projectile, source);
        }
    }
}
