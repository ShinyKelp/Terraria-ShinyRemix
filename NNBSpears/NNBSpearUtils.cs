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
    public static Dictionary<string, int> ModSpearIDs = new Dictionary<string, int>()
    {
        { "MeteorSpearProj", -1 },
        { "CultistSpearProj", -1},
        { "CursedSpearGunProj", -1},
        { "BeetleSpearProj", -1 },
        { "TurtleSpearProj", -1 },
        { "TurtleSpearProj2", -1 },
        { "GladiatorSpearProj", -1 },
        { "TerrariumSpearPro", -1 },
        { "JoterTridentSpear", -1 },
        { "Daybreak", -1 },
        { "SoTC", -1 },
        { "Javelin", -1 },
        { "BoneSpear", -1 },
        { "TonbogiriSpear", -1 }
    };
    public static List<int> VanillaSpears = new List<int>() { ItemID.Spear, ItemID.Trident, ItemID.TheRottedFork, ItemID.ThunderSpear, ItemID.DarkLance, ItemID.Swordfish, ItemID.ObsidianSwordfish, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.Gungnir, ItemID.ChlorophytePartisan, ItemID.NorthPole, ItemID.MonkStaffT2, ItemID.MushroomSpear };
    public static int TRAEJoterTridentItemID = -1;
    public static int TonbogiriItemID = -1;

    public static void SearchModSpears(Mod forMod)
    {
        ModProjectile proj;
        foreach (string modSpearName in NNBSpearUtils.ModSpearIDs.Keys)
        {
            if (forMod.TryFind(modSpearName, out proj))
                NNBSpearUtils.ModSpearIDs[modSpearName] = proj.Type;
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
                NNBSpearUtils.TRAEJoterTridentItemID = joterItem.Type;
        }

        if (ShinyUtils.Consolaria && ModLoader.TryGetMod("Consolaria", out Mod consolariaMod))
        {
            if (consolariaMod.TryFind("TonbogiriSpear", out ModProjectile proj))
                NNBSpearUtils.ModSpearIDs["TonbogiriSpear"] = proj.Type;
            if (consolariaMod.TryFind<ModItem>("Tonbogiri", out ModItem giriItem))
                NNBSpearUtils.TonbogiriItemID = giriItem.Type;
        }
    }

}
