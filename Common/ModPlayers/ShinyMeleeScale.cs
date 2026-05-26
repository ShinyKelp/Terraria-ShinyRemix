using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.Common.ModPlayers
{
    public class ShinyMeleeScale : ModPlayer
    {
        public bool meleeScaleGlove = false;

        public override void ResetEffects()
        {
            meleeScaleGlove = false;
        }
    }
}
