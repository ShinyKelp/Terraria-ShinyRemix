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
                "ThoriumMod", new HashSet<string>()
                {
                    "ChampionsTrifectaShot", 
                    "CinderString" 
                }
            },
            {
                "TRAEProject", new HashSet<string>()
                {
                    "Tribow"
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
            },
            {
                "StormDiversMod", new Dictionary<string, string>()
                {
                    {"HarpyBow", "HarpyArrowProj"},

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
            },
            {
                "TRAEProject", new HashSet<string>()
                {
                    "Trirrow"
                }
            },
            {
                "StormDiversMod", new HashSet<string>()
                {
                    "DesertBowProj",
                    "HellSoulBowProj",
                    "ShroomBowArrowProj",
                    "CultistBowProj",
                    "CultistBowProj2"
                }
            },
            {
                "Consolaria", new HashSet<string>()
                {
                    "VulcanBolt"
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
                SetUpModArrowSynergies(thoriumMod);
            }
            if(ShinyUtils.TRAE && ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                SetUpModArrowSynergies(traeMod);
            }
            if (ShinyUtils.StormDivers && ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
            {
                SetUpModArrowSynergies(stormMod);
            }
            if (ShinyUtils.StormDivers && ModLoader.TryGetMod("Consolaria", out Mod consoleMod))
            {
                SetUpModArrowSynergies(consoleMod);
            }
        }

        private static void SetUpModArrowSynergies(Mod mod) 
        {
            if (ModBowArrowPairNames.ContainsKey(mod.Name))
            {
                foreach (KeyValuePair<string, string> bowPair in ModBowArrowPairNames[mod.Name])
                {
                    ModItem bow;
                    ModProjectile bowShoot;

                    if (mod.TryFind(bowPair.Key, out bow) && mod.TryFind(bowPair.Value, out bowShoot))
                        BowArrowSignatures.Add(bow.Type, bowShoot.Type);
                }
            }

            if (ModComplexBows.ContainsKey(mod.Name))
            {
                foreach (string complexBow in ModComplexBows[mod.Name])
                {
                    ModItem bow;
                    if (mod.TryFind(complexBow, out bow))
                    {
                        BowArrowSignatures.Add(bow.Type, ProjectileID.WoodenArrowFriendly);
                    }
                }
            }
            
            if (ModArrowOverrides.ContainsKey(mod.Name))
            {
                foreach (string arrowName in ModArrowOverrides[mod.Name])
                {
                    ModProjectile arrow;
                    if (mod.TryFind(arrowName, out arrow))
                        ArrowOverrides.Add(arrow.Type);
                }
            }
        }

    }
}
