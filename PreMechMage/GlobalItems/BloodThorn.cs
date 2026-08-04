using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PreMechMage.GlobalItems
{
    public class BloodThorn : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PreMechMage && entity.type == ItemID.SharpTears;
        }

        public override void SetDefaults(Item entity)
        {
            entity.damage += 4;
            entity.ArmorPenetration += 10;
            entity.mana += 4;
        }
    }
}
