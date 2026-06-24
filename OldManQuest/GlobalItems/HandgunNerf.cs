using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OldManQuest.GlobalItems
{
    public class HandgunNerf : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.OldManQuest && entity.type == ItemID.Handgun;
        }

        public override void SetDefaults(Item entity)
        {
            entity.damage -= 3;
        }
    }
}
