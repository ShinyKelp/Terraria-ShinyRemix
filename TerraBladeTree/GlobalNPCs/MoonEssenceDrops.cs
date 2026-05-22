using ShinyRemix.TerraBladeTree.ModItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.TerraBladeTree.GlobalNPCs
{
    public class MoonEssenceDrops : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.BloodNautilus || entity.type == NPCID.GoblinShark || entity.type == NPCID.BloodEelHead;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.BloodEelHead || npc.type == NPCID.GoblinShark)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoonEssence>(), 1, 1, 3));
            }
            else if(npc.type == NPCID.BloodNautilus)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoonEssence>(), 1, 10, 14));
            }
        }

    }
}
