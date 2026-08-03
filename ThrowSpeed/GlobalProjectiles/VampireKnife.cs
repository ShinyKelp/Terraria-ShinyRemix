using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalProjectiles
{
    public class VampireKnife : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.ThrowSpeedScaling && entity.type == ProjectileID.VampireKnife;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.ai[0] = 8f;
            base.OnSpawn(projectile, source);
        }
    }
}
