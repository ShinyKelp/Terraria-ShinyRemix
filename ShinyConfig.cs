using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using static Terraria.ModLoader.ModContent;

namespace ShinyRemix
{
    public class ShinyConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("General")]

        [ReloadRequired]
        [DefaultValue(true)]
        //[Label("text")]
        //[Tooltip("text")]
        public bool SpearRework = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool TerraBladeTree = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool SwordProjectileRates = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool SwordProjectileRanges = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool SwordParries = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool FlailChanges = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool FrostWeaponChanges = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool MeleeArmorChanges = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool SimpleArrowCompatibility = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool OverloadMechanic = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool BlessedMechanic = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool PostMechMimics = true;
    }

    public static class ShinyOptions
    {
        public static bool SpearRework => GetInstance<ShinyConfig>().SpearRework;
        public static bool TerraBladeTree => GetInstance<ShinyConfig>().TerraBladeTree;
        public static bool SwordProjectileRates => GetInstance<ShinyConfig>().SwordProjectileRates;
        public static bool SwordProjectileRanges => GetInstance<ShinyConfig>().SwordProjectileRanges;
        public static bool SwordParries => GetInstance<ShinyConfig>().SwordParries;
        public static bool FlailChanges => GetInstance<ShinyConfig>().FlailChanges;
        public static bool FrostWeaponChanges => GetInstance<ShinyConfig>().FrostWeaponChanges;
        public static bool MeleeArmorChanges => GetInstance<ShinyConfig>().MeleeArmorChanges;
        public static bool SimpleArrowCompatibility => GetInstance<ShinyConfig>().SimpleArrowCompatibility;
        public static bool OverloadMechanic => GetInstance<ShinyConfig>().OverloadMechanic;
        public static bool BlessedMechanic => GetInstance<ShinyConfig>().BlessedMechanic;
        public static bool PostMechMimics => GetInstance<ShinyConfig>().PostMechMimics;
    }
}
