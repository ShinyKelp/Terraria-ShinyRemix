using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordParries.GlobalItems
{
    public class BrandBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordParries && entity.type == ItemID.DD2SquireDemonSword;
        }

        public override void SetDefaults(Item entity)
        {
            entity.useTime += 10;
            entity.useAnimation += 10;
            entity.damage += 50;
        }


    }
}
