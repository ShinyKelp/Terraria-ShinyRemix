using ShinyRemix.TerraBladeTree.ModItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.TerraBladeTree.Systems
{
    public class RecipeChanges : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ShinyOptions.TerraBladeTree)
                return;
            foreach (Recipe recipe in Main.recipe)
            {
                switch (recipe.createItem.type)
                {
                    case ItemID.TerraBlade:
                        if (recipe.HasIngredient(ItemID.BeetleHusk))
                            break;

                        recipe.RemoveIngredient(ItemID.BrokenHeroSword);    //Goes in true excalibur instead

                        //Should change for a ModPlayer flag instead of manual equipment check
                        recipe.AddCondition(new Condition(
                            "Soaring Insignia & Shiny Stone equipped",
                            () =>
                            {
                                Player player = Main.LocalPlayer;
                                bool foundStone = false, foundInsignia = false;
                                for (int i = 3; i < 10; i++)
                                {
                                    if (player.armor[i].type == ItemID.ShinyStone)
                                        foundStone = true;
                                    else if (player.armor[i].type == ItemID.EmpressFlightBooster)
                                        foundInsignia |= true;
                                }
                                return foundStone && foundInsignia && Main.expertMode;
                            }));
                        break;
                    case ItemID.TrueExcalibur:
                        recipe.AddIngredient(ItemID.BrokenHeroSword);
                        recipe.AddIngredient(ItemID.Ectoplasm, 10);
                        break;

                    case ItemID.TrueNightsEdge:
                        recipe.AddIngredient(ModContent.ItemType<MoonEssence>(), 10);
                        break;

                    case ItemID.FieryGreatsword:
                        recipe.AddIngredient(ItemID.Bone, 10);
                        break;

                    case ItemID.BladeofGrass:
                        recipe.AddIngredient(ItemID.BeeWax, 6);
                        break;
                }
            }
        }
        public override void AddRecipes()
        {
            if (!ShinyOptions.TerraBladeTree)
                return;
            //Alternative terra blade recipe for classic worlds
            Recipe recipe = Recipe.Create(ItemID.TerraBlade);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddIngredient(ItemID.TrueExcalibur);
            recipe.AddIngredient(ItemID.TrueNightsEdge);
            recipe.AddIngredient(ItemID.BeetleHusk, 16);
            recipe.AddCondition(new Condition(
                            "Classic mode",
                            () =>
                            {
                                return !Main.expertMode;
                            }));

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
