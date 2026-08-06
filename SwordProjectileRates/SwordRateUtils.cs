using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileRates
{
    public static class SwordRateUtils
    {
        public static Dictionary<int, string> ModdedSwordTypes = new Dictionary<int, string>();
        
        public static Dictionary<string, int> ModdedSwordRates = new Dictionary<string, int>()
        {
            {"DemonBloodSword", 1},
            {"MidasGavel", 1 },
            {"Saba", 2 },
            {"Scalper", 1 },
            {"SoulRender", 1 },
            {"TheBlackBlade", 1 },
            {"TitanSword", 1 },
            {"WhirlpoolSaber", 1 },
            {"WyvernSlayer", 1 }
        };

        public static void SetUpUtils()
        {
            if (ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                foreach(string swordName in ModdedSwordRates.Keys)
                {
                    if(thoriumMod.TryFind(swordName, out ModItem swordItem))
                    {
                        ModdedSwordTypes.Add(swordItem.Type, swordName);
                    }
                }
            }
        }
    }
}
