using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Common.GlobalProjectiles
{
    public class SampleProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return false;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (false)
            {
                Main.NewText($"Spawned: {projectile.Name}, {projectile.type}, {projectile.owner}");
            }
            if(false && source is EntitySource_ItemUse_WithAmmo ammoSource)
            {
                Main.NewText($"Spawned arrow: {projectile.Name}; from bow: {ammoSource.Item.Name}");
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if (false)
            {
                Main.NewText($"Sync AIs: {projectile.ai[0]}, {projectile.ai[1]}, {projectile.ai[2]}");
                Main.NewText($"LocalAIs: {projectile.localAI[0]}, {projectile.localAI[1]}, {projectile.localAI[2]}");
            }
            return base.PreAI(projectile);
        }
    }
}
