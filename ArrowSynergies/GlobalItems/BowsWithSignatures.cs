using Microsoft.Xna.Framework;
using ShinyRemix.ArrowSynergies.GlobalProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.ArrowSynergies.GlobalItems
{
    public class BowsWithSignatures : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public int debuffType = -1;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.SimpleArrowCompatibility && ArrowSynergyUtils.BowArrowSignatures.ContainsKey(entity.type);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (ArrowSynergyUtils.ArrowProjDebuffs.ContainsKey(type))
            {
                debuffType = ArrowSynergyUtils.ArrowProjDebuffs[type];
                type = ArrowSynergyUtils.BowArrowSignatures[item.type];
            }
            else debuffType = -1;

        }
    }
}

