using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.ArrowSynergies
{
    public static class ArrowSynergyUtils
    {

        private static Dictionary<string, HashSet<string>> ModComplexBows = new Dictionary<string, HashSet<string>>()
        {
            {
                "ThoriumMod", new HashSet<string> 
                {
                    "ChampionsTrifectaShot", 
                    "CinderString" 
                }
            }
        };

        private static Dictionary<string, Dictionary<string, string>> ModBowArrowPairNames = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "ThoriumMod", new Dictionary<string, string>()
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
                }
            }
        };
        //Add consolaria vulcan

        private static Dictionary<string, HashSet<string>> ModArrowOverrides = new Dictionary<string, HashSet<string>>()
        {
            {
                "ThoriumMod", new HashSet<string>()
                {
                    "CometCrossfirePro",
                    "ShadowFlareBowPro",
                    "CinderStringPro",
                    "ChampionsTrifectaShotPro",
                    "ChampionsTrifectaShotPro2",
                    "ChampionsTrifectaShotPro3",
                    "ChampionsTrifectaShotPro4"
                }
            }
        };

        public static Dictionary<int, int> BowArrowSignatures = new Dictionary<int, int>()
        {
            {ItemID.HellwingBow, ProjectileID.Hellwing},
            {ItemID.FairyQueenRangedItem, ProjectileID.FairyQueenRangedItemShot},
            {ItemID.MoltenFury, ProjectileID.FireArrow},
            //{ItemID.DD2PhoenixBow, ProjectileID.FireArrow}
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

        public static void SetUpUtils()
        {
            if (ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                foreach (KeyValuePair<string, string> bowPair in ModBowArrowPairNames["ThoriumMod"])
                {
                    ModItem bow;
                    ModProjectile bowShoot;

                    if (thoriumMod.TryFind(bowPair.Key, out bow) && thoriumMod.TryFind(bowPair.Value, out bowShoot))
                        BowArrowSignatures.Add(bow.Type, bowShoot.Type);

                }
                foreach (string complexBow in ModComplexBows["ThoriumMod"])
                {
                    ModItem bow;
                    if (thoriumMod.TryFind(complexBow, out bow))
                    {
                        BowArrowSignatures.Add(bow.Type, ProjectileID.WoodenArrowFriendly);
                    }
                }

                foreach (string arrowName in ModArrowOverrides["ThoriumMod"])
                {
                    ModProjectile arrow;
                    if (thoriumMod.TryFind(arrowName, out arrow))
                        ArrowOverrides.Add(arrow.Type);
                }

            }
        }

    }
}
