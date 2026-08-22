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

namespace ShinyRemix.BiomeWeapons.GlobalItems
{
    public class CorruptorScourge : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ItemID.ScourgeoftheCorruptor;
        }
        public override void SetDefaults(Item entity)
        {
            if (ShinyUtils.TRAE && NNBSpearUtils.ModSpearProjIDs["SoTC"] != -1)
            {
                entity.useTime = entity.useAnimation = 33;
                entity.damage -= 5;
                PrefixLegacy.ItemSets.SwordsHammersAxesPicks[entity.type] = true;
            }
        }
    }
}
