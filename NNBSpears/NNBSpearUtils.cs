using ShinyRemix.Flails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears;

internal static class NNBSpearUtils
{
    private static Dictionary<string, HashSet<string>> ModSpearProjNames = new Dictionary<string, HashSet<string>>()
    {
        {
            "StormDiversMod", new HashSet<string>()
            {
                "MeteorSpearProj",
                "CultistSpearProj",
                "CursedSpearGunProj",
                "BeetleSpearProj",
                "TurtleSpearProj",
                "TurtleSpearProj2",
                "GladiatorSpearProj"
            }
        },
        {
            "ThoriumMod", new HashSet<string>()
            {
                "TerrariumSpearPro"
            }
        },
        {
            "TRAEProject", new HashSet<string>()
            {
                "JoterTridentSpear",
                "Daybreak",
                "SoTC",
                "Javelin",
                "BoneSpear",
            }
        },
        {
            "Consolaria", new HashSet<string>()
            {
                "TonbogiriSpear"
            }
        }
    };

    public static Dictionary<string, int> ModSpearProjIDs = new Dictionary<string, int>() { };
    public static List<int> VanillaSpears = new List<int>() { ItemID.Spear, ItemID.Trident, ItemID.TheRottedFork, ItemID.ThunderSpear, ItemID.DarkLance, ItemID.Swordfish, ItemID.ObsidianSwordfish, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.Gungnir, ItemID.ChlorophytePartisan, ItemID.NorthPole, ItemID.MonkStaffT2, ItemID.MushroomSpear };
    public static int TRAEJoterTridentItemID = -1;
    public static int TonbogiriItemID = -1;

    public static void SearchModSpears(Mod forMod)
    {
        ModProjectile proj;
        foreach (string modSpearName in NNBSpearUtils.ModSpearProjNames[forMod.Name])
        {
            if (forMod.TryFind(modSpearName, out proj))
            {
                ModSpearProjIDs.Add(modSpearName, proj.Type);
            }
        }
    }

    public static void SetUpUtils()
    {
        if (ShinyUtils.StormDivers && ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
        {
            SearchModSpears(stormMod);
        }

        if (ShinyUtils.Thorium && ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
        {
            SearchModSpears(thoriumMod);

        }
        if (ShinyUtils.TRAE && ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
        {
            SearchModSpears(traeMod);
            if (traeMod.TryFind<ModItem>("JoterTrident", out ModItem joterItem))
                TRAEJoterTridentItemID = joterItem.Type;
        }

        if (ShinyUtils.Consolaria && ModLoader.TryGetMod("Consolaria", out Mod consolariaMod))
        {
            if (consolariaMod.TryFind("TonbogiriSpear", out ModProjectile proj))
                ModSpearProjIDs["TonbogiriSpear"] = proj.Type;
            if (consolariaMod.TryFind<ModItem>("Tonbogiri", out ModItem giriItem))
                TonbogiriItemID = giriItem.Type;
        }
    }

}
