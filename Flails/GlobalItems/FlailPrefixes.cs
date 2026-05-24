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

namespace ShinyRemix.Flails.GlobalItems
{
    public class FlailPrefixes : GlobalItem
    {

        static List<int> VanillaFlailIDs = new List<int>
        {
            ItemID.Mace,
            ItemID.FlamingMace,
            ItemID.BallOHurt,
            ItemID.TheMeatball,
            ItemID.BlueMoon,
            ItemID.Sunfury,
            ItemID.DaoofPow,
            ItemID.DripplerFlail,
            ItemID.FlowerPow
        };

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
      
            return VanillaFlailIDs.Contains(entity.type) && !ShinyUtils.TRAE;
        }

        public override void SetDefaults(Item entity)
        {
            base.SetDefaults(entity);
            if(VanillaFlailIDs.Contains(entity.type))
                PrefixLegacy.ItemSets.SwordsHammersAxesPicks[entity.type] = true;
        }
    }
}
