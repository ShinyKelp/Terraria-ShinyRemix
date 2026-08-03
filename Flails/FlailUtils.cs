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
        public static int VaporizerFlailID = -1;
        public static int FullMoonID = -1;
        public static Dictionary<string, int> ModFlailItems = new Dictionary<string, int>()
        {
            {"BoneFlayerTail",-1},
            {"EbonyTail",-1},
            {"FleshMace",-1},
            {"LivewireCrasher",-1},
            {"LodeStoneBreaker",-1},
            {"SparkingJellyBall",-1},
            {"StarTrail",-1},
            {"SteamFlail",-1},
            {"TheJuggernaut",-1},
            {"TheSeaMine",-1},
            {"TheSnowball",-1}
        };

        public static void SetUpUtils()
        {
            if(ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                foreach(string itemName in ModFlailItems.Keys)
                {
                    if (thoriumMod.TryFind(itemName, out ModItem thoriumItem))
                        ModFlailItems[itemName] = thoriumItem.Type;
                }
            }
            if (ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
            {
                if (stormMod.TryFind("DestroyerFlailProj", out ModProjectile proj))
                    FlailUtils.VaporizerFlailID = proj.Type;
            }

            if (ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                if (traeMod.TryFind<ModProjectile>("FullMoonP", out ModProjectile proj))
                    FlailUtils.FullMoonID = proj.Type;
            }
        }

    }
}
