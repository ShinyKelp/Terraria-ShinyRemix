using MonoMod.RuntimeDetour;
using ShinyRemix.Flails;
using ShinyRemix.NNBSpears;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using System;
using System.Globalization;
using ShinyRemix.ArrowSynergies;
using System.Collections.Generic;

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
                ModProjectile proj;
                foreach(string modSpearName in NNBSpearUtils.ModSpearIDs.Keys)
                {
                    if(stormMod.TryFind(modSpearName, out proj))
                        NNBSpearUtils.ModSpearIDs[modSpearName] = proj.Type;
                    if(stormMod.TryFind("DestroyerFlailProj", out proj))
                        FlailUtils.VaporizerFlailID = proj.Type;
                }
            }
            if(ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                ShinyUtils.Thorium = true;
                ModProjectile proj;
                foreach (string modSpearName in NNBSpearUtils.ModSpearIDs.Keys)
                {
                    if (thoriumMod.TryFind(modSpearName, out proj))
                        NNBSpearUtils.ModSpearIDs[modSpearName] = proj.Type;
                }


                foreach (KeyValuePair<string, string> bowPair in ArrowSynergyUtils.ModBowArrowPairs)
                {
                    ModItem bow;
                    ModProjectile bowShoot;

                    if(thoriumMod.TryFind(bowPair.Key, out bow) && thoriumMod.TryFind(bowPair.Value, out bowShoot))
                        ArrowSynergyUtils.BowArrowSignatures.Add(bow.Type, bowShoot.Type);
                    
                }
                foreach(string complexBow in ArrowSynergyUtils.ComplexBows)
                {
                    ModItem bow;
                    if (thoriumMod.TryFind(complexBow, out bow))
                    {
                        ArrowSynergyUtils.BowArrowSignatures.Add(bow.Type, ProjectileID.WoodenArrowFriendly);
                    }
                }

                foreach(string arrowName in ArrowSynergyUtils.ModArrowOverrides)
                {
                    ModProjectile arrow;
                    if (thoriumMod.TryFind(arrowName, out arrow))
                        ArrowSynergyUtils.ArrowOverrides.Add(arrow.Type);
                }

            }
            if (ModLoader.TryGetMod("TRAEProject", out Mod traeMod))
            {
                ShinyUtils.TRAE = true;
                ModProjectile proj;
                foreach (string modSpearName in NNBSpearUtils.ModSpearIDs.Keys)
                {
                    if (traeMod.TryFind(modSpearName, out proj))
                        NNBSpearUtils.ModSpearIDs[modSpearName] = proj.Type;
                }
                if(traeMod.TryFind<ModItem>("JoterTrident", out ModItem joterItem))
                    NNBSpearUtils.TRAEJoterTridentItemID = joterItem.Type;
                if(traeMod.TryFind<ModProjectile>("FullMoonP", out proj))
                    FlailUtils.FullMoonID = proj.Type;

            }

            if(ModLoader.TryGetMod("Consolaria", out Mod consolariaMod))
            {
                ShinyUtils.Consolaria = true;
                ModProjectile proj;
                foreach (string modSpearName in NNBSpearUtils.ModSpearIDs.Keys)
                {
                    if (consolariaMod.TryFind(modSpearName, out proj))
                        NNBSpearUtils.ModSpearIDs[modSpearName] = proj.Type;
                }
                if (consolariaMod.TryFind<ModItem>("Tonbogiri", out ModItem giriItem))
                    NNBSpearUtils.TonbogiriItemID = giriItem.Type;
            }
        }
	}
}