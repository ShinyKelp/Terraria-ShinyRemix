using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PirateInvasionBuffs.GlobalNPCs
{
    public class NormalPirateLoots : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return ShinyOptions.PirateInvasionBuffs && (entity.type == NPCID.PirateCrossbower || entity.type == NPCID.PirateDeadeye || entity.type == NPCID.PirateCorsair
                || entity.type == NPCID.PirateDeckhand || entity.type == NPCID.PirateCaptain || entity.type == NPCID.PirateShip);
        }

        //1.4.5 to-do: barrel launcher changes
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            var rules = npcLoot.Get(includeGlobalDrops: false);
            foreach (var rule in rules) 
            {
                if (rule is CommonDrop commonDrop && (commonDrop.itemId == ItemID.Cutlass || commonDrop.itemId == ItemID.CoinGun || commonDrop.itemId == ItemID.PirateStaff))
                {
                    npcLoot.Remove(rule);
                }
            }
            if(npc.type == NPCID.PirateCaptain)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.CoinGun, 10));
                npcLoot.Add(ItemDropRule.Common(ItemID.Cutlass, 3));
            }
            if(npc.type == NPCID.PirateShip)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.PirateStaff, 5));
            }
        }
    }
}
