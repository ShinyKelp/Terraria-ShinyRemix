using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.TerraBladeTree.ModItems
{
    public class MoonEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 4000;
            Item.maxStack = 9999;
        }
    }
}
