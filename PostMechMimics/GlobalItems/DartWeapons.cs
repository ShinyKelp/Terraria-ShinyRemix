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
    public class DartWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && (entity.type == ItemID.DartPistol || entity.type == ItemID.DartRifle);
        }

        public override void SetDefaults(Item entity)
        {
            if(entity.type == ItemID.DartPistol)
            {
                entity.useTime -= 3;
                entity.useAnimation -= 3;
            }
            else if (entity.type == ItemID.DartRifle)
            {
                entity.damage += 6;
            }
        }
    }
}
