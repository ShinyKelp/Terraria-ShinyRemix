using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace ShinyRemix.ThrowSpeed
{
    public static class ThrowSpeedUtils
    {
        public static HashSet<int> ThrowItems = new HashSet<int>()
        {
            ItemID.ShadowFlameKnife,
            ItemID.VampireKnives,
            ItemID.ScourgeoftheCorruptor,
            ItemID.PossessedHatchet,
            ItemID.DayBreak
        };
    }
}
