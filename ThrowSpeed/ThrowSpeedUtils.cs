using Humanizer;
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
            {
                "StormDiversMod", new HashSet<string>()
                {
                    "BoneBoomerang"
                }
            },
            {
                "ThoriumMod", new HashSet<string>()
                {
                    "BentZombieArm",
                    "ClimbersIceAxe",
                    "ColdFront",
                    "GiantGlowstick",
                    "GraniteReflector",
                    "OceansJudgement",
                    "ShipsHelm",
                    "TerrariumHyperDisc",
                    "ThoriumBoomerang",
                    "TitanBoomerang"

                }
            }
        };

        public static HashSet<int> ThrowItems = new HashSet<int>()
        {
            ItemID.ShadowFlameKnife,
            ItemID.VampireKnives,
            ItemID.ScourgeoftheCorruptor,
            ItemID.PossessedHatchet,
            ItemID.DayBreak,
            ItemID.Trimarang,
            ItemID.WoodenBoomerang,
            ItemID.EnchantedBoomerang,
            ItemID.IceBoomerang,
            ItemID.Shroomerang,
            ItemID.ThornChakram,
            ItemID.Flamarang,
            ItemID.Bananarang,
            ItemID.LightDisc,
            ItemID.FruitcakeChakram
        };

        private static void SetUpMod(Mod mod)
        {
            foreach (string itemName in ModThrowItemNames[mod.Name])
            {
                if (mod.TryFind(itemName, out ModItem modItem))
                {
                    ThrowItems.Add(modItem.Type);
                }
            }
        }

        public static void SetUpUtils()
        {
            if (ShinyUtils.TRAE && ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                SetUpMod(traeMod);
            }
            if (ShinyUtils.TRAE && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                SetUpMod(thoriumMod);
            }
            if (ShinyUtils.TRAE && ModLoader.TryGetMod("StormDiversMod", out Mod stormDiversMod))
            {
                SetUpMod(stormDiversMod);
            }
        }
    }
}
