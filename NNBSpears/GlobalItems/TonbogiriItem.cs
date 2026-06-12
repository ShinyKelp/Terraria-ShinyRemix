using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalItems
{
    public class TonbogiriItem : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return ShinyOptions.SpearRework && ShinyUtils.Consolaria && item.type == NNBSpearUtils.TonbogiriItemID;
        }

        //Tonbogiri looks very slow with overriden animation, and has less DPS than Gungnir with new i-frame scaling.
        public override void SetDefaults(Item item)
        {
            item.useTime = item.useAnimation = 26;
            item.knockBack = 8f;
        }
    }
}
