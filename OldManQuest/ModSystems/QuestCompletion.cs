using ShinyRemix.TerraBladeTree.ModItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShinyRemix.OldManQuest.ModSystems
{
    public class QuestCompletion : ModSystem
    {
        public static List<string> CompletedPlayers = new List<string>();
        public override void ClearWorld()
        {
            CompletedPlayers.Clear();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["OldManCompletedPlayers"] = CompletedPlayers;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            CompletedPlayers = tag.GetList<string>("OldManCompletedPlayers").ToList();
        }

        public override void PostAddRecipes()
        {
            if (!ShinyOptions.OldManQuest)
                return;
            foreach (Recipe recipe in Main.recipe)
            {
                switch (recipe.createItem.type)
                {
                    case ItemID.NightsEdge:
                    case ItemID.PhoenixBlaster:
                        recipe.AddIngredient(ItemID.Bone, 20);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
