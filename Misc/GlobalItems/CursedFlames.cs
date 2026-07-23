using ShinyRemix.Misc.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Misc.GlobalItems
{
    public class CursedFlames : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CursedFlames;
        }
        public override void SetDefaults(Item entity)
        {
            entity.shoot = ModContent.ProjectileType<CursedFlamesStream>();
            entity.useTime = 5;
            entity.useAnimation = 20;
            entity.damage -= 8;
        }
    }
}
