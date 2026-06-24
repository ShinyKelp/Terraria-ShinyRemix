using MonoMod.RuntimeDetour;
using ShinyRemix.Flails;
using ShinyRemix.NNBSpears;
using Terraria.ModLoader;

namespace ShinyRemix
{
	public class ShinyRemix : Mod
	{
        static ILHook ShieldHook;

        public override void Load()
        {
            ShieldHook = new ILHook(typeof(Terraria.Player).GetMethod("ItemCheck_ManageRightClickFeatures_ShieldRaise"), SwordParries.SwordParriesIL.EnableSwordParries);
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