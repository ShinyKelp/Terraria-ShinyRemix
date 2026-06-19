using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics
{
    public class KeyRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ShinyOptions.PostMechMimics)
                return;
            foreach (Recipe recipe in Main.recipe)
            {
                switch (recipe.createItem.type)
                {
                    case ItemID.LightKey:
                        recipe.RemoveIngredient(ItemID.SoulofLight);
                        recipe.AddIngredient(ItemID.SoulofLight, 5);
                        recipe.AddIngredient(ItemID.HallowedBar, 1);
                        break;
                    case ItemID.NightKey:
                        recipe.RemoveIngredient(ItemID.SoulofNight);
                        recipe.AddIngredient(ItemID.SoulofNight, 5);
                        recipe.AddIngredient(ItemID.HallowedBar, 1);
                        break;
                }
            }
        }
    }
}
