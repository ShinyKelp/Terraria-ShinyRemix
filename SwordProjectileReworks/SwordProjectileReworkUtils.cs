using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks
{
    public static class SwordProjectileReworkUtils
    {
        public static int TizonaProjType = -1;
        public static void SetUpUtils()
        {
            if (ShinyUtils.Consolaria && ModLoader.TryGetMod("Consolaria", out Mod consolariaMod))
            {
                if (consolariaMod.TryFind("TizonaShoot", out ModProjectile tizonaProj))
                    TizonaProjType = tizonaProj.Type;
            }
        }
    }
}
