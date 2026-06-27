using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace ShinyRemix
{
    public class ShinyUtils
    {
        public static bool StormDivers = false;
        public static bool Thorium = false;
        public static bool TRAE = false;
        public static bool Consolaria = true;
        public static List<int> SizePrefixes = new List<int>()
        {
            PrefixID.Large,
            PrefixID.Massive,
            PrefixID.Savage,
            PrefixID.Bulky,
            PrefixID.Shameful,
            PrefixID.Dangerous,
            PrefixID.Small,
            PrefixID.Unhappy,
            PrefixID.Terrible,
            PrefixID.Tiny,
            PrefixID.Legendary
        };
        public static List<int> SpeedPrefixes = new List<int>()
        {
            PrefixID.Quick,
            PrefixID.Deadly,
            PrefixID.Agile,
            PrefixID.Nimble,
            PrefixID.Murderous,
            PrefixID.Slow,
            PrefixID.Sluggish,
            PrefixID.Lazy,
            PrefixID.Annoying,
            PrefixID.Nasty,
            PrefixID.Light,
            PrefixID.Legendary,
            PrefixID.Unhappy,
            PrefixID.Heavy,
            PrefixID.Bulky,
            PrefixID.Rapid,
            PrefixID.Frenzying,
            PrefixID.Hasty,
            PrefixID.Unreal,
            PrefixID.Deadly,
            PrefixID.Awkward,
            PrefixID.Powerful,
            PrefixID.Lethargic,
            PrefixID.Taboo,
            PrefixID.Manic,
            PrefixID.Mythical,
            PrefixID.Celestial
        };
    }
}
