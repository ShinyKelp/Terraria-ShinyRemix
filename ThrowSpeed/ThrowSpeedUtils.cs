using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.ThrowSpeed
{
    public static class ThrowSpeedUtils
    {
        private static Dictionary<string, HashSet<string>> ModThrowItemNames = new Dictionary<string, HashSet<string>>()
        {
            {
                "TRAEProject", new HashSet<string>()
                {
                    "Wingspan",
                    "WingspanBlue"
                }
            },
        };

        public static HashSet<int> ThrowItems = new HashSet<int>()
        {
            ItemID.ShadowFlameKnife,
            ItemID.VampireKnives,
            ItemID.ScourgeoftheCorruptor,
            ItemID.PossessedHatchet,
            ItemID.DayBreak,
            ItemID.Trimarang
        };

        public static void SetUpUtils()
        {
            if (ShinyUtils.TRAE && ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                foreach (string itemName in ModThrowItemNames["TRAEProject"])
                {
                    if(traeMod.TryFind(itemName, out ModItem modItem))
                    {
                        ThrowItems.Add(modItem.Type);
                    }
                }
            }
        }
    }
}
