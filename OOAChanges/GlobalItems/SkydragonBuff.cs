using ShinyRemix.NNBSpears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.GlobalItems
{
    public class SkydragonBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ItemID.MonkStaffT3;
        }

        public override void SetStaticDefaults()
        {
            PrefixLegacy.ItemSets.SwordsHammersAxesPicks[ItemID.MonkStaffT3] = true;
        }

        public override void SetDefaults(Item entity)
        {
            entity.DamageType = DamageClass.Melee;
        }
    }
}
