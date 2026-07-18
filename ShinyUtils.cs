using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

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
            PrefixID.Deadly2,
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

        static Dictionary<int, float> VanillaSpeedPrefixValues = new Dictionary<int, float>()
        {
            {PrefixID.Rapid, 1.15f},
            {PrefixID.Light, 1.15f},
            {PrefixID.Frenzying, 1.15f},
            {PrefixID.Legendary, 1.10f},
            {PrefixID.Unreal, 1.10f},
            {PrefixID.Mythical, 1.10f},
            {PrefixID.Hasty, 1.10f},
            {PrefixID.Deadly, 1.10f},
            {PrefixID.Agile, 1.10f},
            {PrefixID.Quick, 1.10f},
            {PrefixID.Manic, 1.10f},
            {PrefixID.Taboo, 1.10f},
            {PrefixID.Nasty, 1.10f},
            {PrefixID.Murderous, 1.06f},
            {PrefixID.Deadly2, 1.05f},
            {PrefixID.Nimble, 1.05f},
            {PrefixID.Lazy, 0.92f},
            {PrefixID.Celestial, 0.9f},
            {PrefixID.Powerful, 0.9f},
            {PrefixID.Heavy, 0.9f},
            {PrefixID.Unhappy, 0.9f},
            {PrefixID.Awkward, 0.9f},
            {PrefixID.Bulky, 0.85f},
            {PrefixID.Slow, 0.85f},
            {PrefixID.Lethargic, 0.85f},
            {PrefixID.Annoying, 0.85f},
            {PrefixID.Sluggish, 0.85f}
        };

        public static float GetPrefixSpeedModifier(int prefixID)
        {
            if (prefixID <= 0)
                return 1f;
            else if(VanillaSpeedPrefixValues.ContainsKey(prefixID))
                return VanillaSpeedPrefixValues[prefixID];
            else
            {
                ModPrefix prefix = PrefixLoader.GetPrefix(prefixID);
                if (prefix == null)
                    return 1f;

                float damageMult, knockbackMult, useTimeMult, scaleMult, shotSpeedMult, manaUseMult;
                damageMult = knockbackMult = useTimeMult = scaleMult = shotSpeedMult = manaUseMult = 1f;
                int critBonus = 4;
                prefix.SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shotSpeedMult, ref manaUseMult, ref critBonus);
                return 1f / useTimeMult;
            }
        }
    }
}
