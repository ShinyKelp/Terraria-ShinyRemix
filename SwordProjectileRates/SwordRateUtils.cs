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
        public static Dictionary<int, int> ModSwordRates = new Dictionary<int, int>();

        private static Dictionary<string, Dictionary<string, int>> ModSwordRateNames = new Dictionary<string, Dictionary<string, int>>
        {
            {
                "ThoriumMod", new Dictionary<string, int>()
                {
                    {"DemonBloodSword", 1},
                    {"MidasGavel", 1 },
                    {"Saba", 2 },
                    {"Scalper", 1 },
                    {"SoulRender", 1 },
                    {"TheBlackBlade", 1 },
                    {"TitanSword", 1 },
                    {"WhirlpoolSaber", 1 },
                    {"WyvernSlayer", 1 },
                }
            },
            {
                "StormDiversMod", new Dictionary<string, int>()
                {
                    {"OceanSword", 1 },
                    {"EyeSword", 1 },
                    {"HellSoulSword", 1 },
                    {"SpaceRockSword", 1 },
                    {"LightDarkSword", 1 },
                }
            },
            {
                "TRAEProject", new Dictionary<string, int>()
                {
                    {"HeadHarvester", 1 }
                }
            }
        };

        public static void SetUpUtils()
        {
            if (ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                RegisterModSwords(thoriumMod);
            }
            if (ShinyUtils.StormDivers && ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
            {
                RegisterModSwords(stormMod);
            }
            if(ShinyUtils.TRAE && ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                RegisterModSwords(traeMod);
            }
        }

        private static void RegisterModSwords(Mod mod)
        {
            foreach (string swordName in ModSwordRateNames[mod.Name].Keys)
            {
                if (mod.TryFind(swordName, out ModItem swordItem))
                {
                    ModSwordRates.Add(swordItem.Type, ModSwordRateNames[mod.Name][swordName]);
                }
            }
        }
    }
}
