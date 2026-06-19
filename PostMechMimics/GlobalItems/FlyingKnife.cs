using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.GlobalItems
{
    public class FlyingKnife : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ItemID.FlyingKnife;
        }
        public override void SetDefaults(Item entity)
        {
            entity.damage -= 5;
        }
    }
}
