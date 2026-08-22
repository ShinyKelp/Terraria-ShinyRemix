using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.Flails
{
    public static class FlailUtils
    {

        private static Dictionary<string, HashSet<string>> ModFlailItemNames = new Dictionary<string, HashSet<string>>()
        {
            {
                "ThoriumMod", new HashSet<string>()
                {
                    "BoneFlayerTail",
                    "EbonyTail",
                    "FleshMace",
                    "LivewireCrasher",
                    "LodeStoneBreaker",
                    "SparkingJellyBall",
                    "StarTrail",
                    "SteamFlail",
                    "TheJuggernaut",
                    "TheSeaMine",
                    "TheSnowball"
                }
            }
        };

        public static int VaporizerFlailProjID = -1;
        public static int FullMoonProjID = -1;

        public static HashSet<int> ModFlailItemIDs = new HashSet<int>();

        public static void SetUpUtils()
        {
            if(ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                foreach(string itemName in ModFlailItemNames["ThoriumMod"])
                {
                    if (thoriumMod.TryFind(itemName, out ModItem thoriumItem))
                        ModFlailItemIDs.Add(thoriumItem.Type);
                }
            }
            if (ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
            {
                if (stormMod.TryFind("DestroyerFlailProj", out ModProjectile proj))
                    VaporizerFlailProjID = proj.Type;
            }

            if (ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                if (traeMod.TryFind<ModProjectile>("FullMoonP", out ModProjectile proj))
                    FullMoonProjID = proj.Type;
            }
        }

    }
}
