using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalItems
{
    public class SpearItems : GlobalItem
    {
        //Add autoswing to all spears.
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return NNBSpearUtils.VanillaSpears.Contains(item.type);
        }

        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
        }

        //Add size and speed reforges
        public override void SetStaticDefaults()
        {
            for (int i = 0; i < NNBSpearUtils.VanillaSpears.Count; i++)
            {
                PrefixLegacy.ItemSets.SwordsHammersAxesPicks[NNBSpearUtils.VanillaSpears[i]] = true;
            }
        }

    }
}
