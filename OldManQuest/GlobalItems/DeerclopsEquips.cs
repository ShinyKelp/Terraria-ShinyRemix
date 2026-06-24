using ShinyRemix.OldManQuest.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OldManQuest.GlobalItems
{
    public class DeerclopsEquips : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.DeerclopsMask || entity.type == ItemID.BoneHelm;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<PlayerDarkItem>().darkItemEquipped = true;
        }
    }
}
