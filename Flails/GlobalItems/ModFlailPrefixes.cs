using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Prefixes;
using Terraria.ModLoader;

namespace ShinyRemix.Flails.GlobalItems
{
    public class ModFlailPrefixes : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return FlailUtils.ModFlailItemIDs.Contains(entity.type);
        }

        public override void SetDefaults(Item entity)
        {
            entity.DamageType = DamageClass.Melee;  //Allows speed prefixes, but not size
        }
    }
}
