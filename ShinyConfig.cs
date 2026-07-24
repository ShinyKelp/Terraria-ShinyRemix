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
        public bool SwordProjectileReworks = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool SwordParries = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool FlailChanges = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Misc = true;

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
        public bool PreMechMage = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool BlessedMechanic = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool PostMechMimics = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool BiomeKeyWeapons = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool OldOneArmyBuffs = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool OldManQuest = true;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool LifestealRework = true;
    }

    public static class ShinyOptions
    {
        public static bool SpearRework => GetInstance<ShinyConfig>().SpearRework;
        public static bool TerraBladeTree => GetInstance<ShinyConfig>().TerraBladeTree;
        public static bool SwordProjectileRates => GetInstance<ShinyConfig>().SwordProjectileRates;
        public static bool SwordProjectileReworks => GetInstance<ShinyConfig>().SwordProjectileReworks;
        public static bool SwordParries => GetInstance<ShinyConfig>().SwordParries;
        public static bool FlailChanges => GetInstance<ShinyConfig>().FlailChanges;
        public static bool MeleeArmorChanges => GetInstance<ShinyConfig>().MeleeArmorChanges;
        public static bool SimpleArrowCompatibility => GetInstance<ShinyConfig>().SimpleArrowCompatibility;
        public static bool OverloadMechanic => GetInstance<ShinyConfig>().OverloadMechanic;
        public static bool BlessedMechanic => GetInstance<ShinyConfig>().BlessedMechanic;
        public static bool PreMechMage => GetInstance<ShinyConfig>().PreMechMage;
        public static bool PostMechMimics => GetInstance<ShinyConfig>().PostMechMimics;
        public static bool BiomeKeyWeapons => GetInstance<ShinyConfig>().BiomeKeyWeapons;
        public static bool OldOneArmyBuffs => GetInstance<ShinyConfig>().OldOneArmyBuffs;
        public static bool OldManQuest => GetInstance<ShinyConfig>().OldManQuest;
        public static bool LifestealRework => GetInstance<ShinyConfig>().LifestealRework;
        public static bool Misc => GetInstance<ShinyConfig>().Misc;

    }
}
