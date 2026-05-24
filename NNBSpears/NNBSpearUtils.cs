using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

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
        { "BoneSpear", -1 }
    };
    public static List<int> VanillaSpears = new List<int>() { ItemID.Spear, ItemID.Trident, ItemID.TheRottedFork, ItemID.ThunderSpear, ItemID.DarkLance, ItemID.Swordfish, ItemID.ObsidianSwordfish, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.Gungnir, ItemID.ChlorophytePartisan, ItemID.NorthPole, ItemID.MonkStaffT2, ItemID.MushroomSpear };
    public static int TRAEJoterTridentItemID = -1;

}
