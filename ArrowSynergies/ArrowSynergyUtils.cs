using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace ShinyRemix.ArrowSynergies
{
    public static class ArrowSynergyUtils
    {
        public static Dictionary<int, int> BowArrowSignatures = new Dictionary<int, int>()
        {
            {ItemID.HellwingBow, ProjectileID.Hellwing},
            {ItemID.FairyQueenRangedItem, ProjectileID.FairyQueenRangedItemShot},
            {ItemID.MoltenFury, ProjectileID.FireArrow},
            //{ItemID.DD2PhoenixBow, ProjectileID.FireArrow}
        };

        public static HashSet<string> ComplexBows = new HashSet<string>()
        {
            "ChampionsTrifectaShot", "CinderString"
        };



        public static Dictionary<string, string> ModBowArrowPairs = new Dictionary<string, string>()
        {
            {"BloomingBow", "BloomingBowPro"},
            {"TalonBurst","TalonBurstPro"},
            {"CoralCrossbow","CoralCrossbowPro"},
            {"FeatherFoe","FeatherArrowPro"},
            {"GraniteCrossbow","GraniteArrowPro"},
            {"GrassStringBow","JungleArrow"},
            {"DecayingSorrow","DecayingSorrowPro"},
            {"CupidString","CupidStringPro"},
            {"GlacialSting","GlacialArrow"},
            {"ShusWrath","ShusArrow"},
        };


        public static HashSet<string> ModArrowOverrides = new HashSet<string>()
        {
            "CometCrossfirePro",
            "ShadowFlareBowPro",
            "CinderStringPro",
            "ChampionsTrifectaShotPro",
            "ChampionsTrifectaShotPro2",
            "ChampionsTrifectaShotPro3",
            "ChampionsTrifectaShotPro4",
        };

        public static HashSet<int> ArrowOverrides = new HashSet<int>()
        {
            ProjectileID.FrostArrow,
            ProjectileID.BoneArrow,
            ProjectileID.ShadowFlameArrow,
            ProjectileID.PulseBolt,
            ProjectileID.BloodArrow,
            ProjectileID.DD2BetsyArrow
        };

        public static readonly Dictionary<int, int> ArrowProjDebuffs = new()
        {
            { ProjectileID.FireArrow, BuffID.OnFire },
            { ProjectileID.FrostburnArrow, BuffID.Frostburn },
            { ProjectileID.IchorArrow, BuffID.Ichor },
            { ProjectileID.CursedArrow, BuffID.CursedInferno },
            { ProjectileID.VenomArrow, BuffID.Venom },
        };

        public static readonly Dictionary<int, int> ArrowItemDebuffs = new()
        {
            { ItemID.FlamingArrow, BuffID.OnFire },
            { ItemID.FrostburnArrow, BuffID.Frostburn },
            { ItemID.IchorArrow, BuffID.Ichor },
            { ItemID.CursedArrow, BuffID.CursedInferno },
            { ItemID.VenomArrow, BuffID.Venom },
        };
    }
}
