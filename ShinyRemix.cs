using MonoMod.RuntimeDetour;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using System;
using System.Globalization;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using ShinyRemix.ThrowSpeed.GlobalItems;

namespace ShinyRemix
{
	public class ShinyRemix : Mod
	{

        public override void Load()
        {
            IL_Player.ItemCheck_ManageRightClickFeatures_ShieldRaise += SwordParries.SwordParriesIL.EnableSwordParries;
            On_DD2Event.StartInvasion += OOAChanges.OOAWaveSkip.OOAStartWaveSkip;
            IL_Main.UpdateTime_StartDay += PirateInvasionBuffs.PirateInvasionIL.ChangePirateInvasionCheck;
        }

        public override void PostSetupContent()
		{
            if (ModLoader.TryGetMod("StormDiversMod", out Mod stormMod))
            {
                ShinyUtils.StormDivers = true;
            }
            if(ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                ShinyUtils.Thorium = true;
            }
            if (ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                ShinyUtils.TRAE = true;
            }

            if(ModLoader.TryGetMod("Consolaria", out Mod consolariaMod))
            {
                ShinyUtils.Consolaria = true;
            }

            NNBSpears.NNBSpearUtils.SetUpUtils();
            Flails.FlailUtils.SetUpUtils();
            ArrowSynergies.ArrowSynergyUtils.SetUpUtils();
            SwordProjectileRates.SwordRateUtils.SetUpUtils();
            SwordProjectileReworks.SwordProjectileReworkUtils.SetUpUtils();
            ThrowSpeed.ThrowSpeedUtils.SetUpUtils();
        }
	}
}