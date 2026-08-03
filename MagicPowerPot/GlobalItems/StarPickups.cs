using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.MagicPowerPot.GlobalItems
{
    public class StarPickups : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.MagicPowerBoost && (entity.type == ItemID.Star || entity.type == ItemID.SoulCake || entity.type == ItemID.SugarPlum);
        }

        public override bool OnPickup(Item item, Player player)
        {
            if(!ShinyUtils.TRAE && player.HasBuff(BuffID.MagicPower))
            {
                player.statMana += 50;
                player.statMana = Math.Min(player.statMana, player.statManaMax2);
                return false;
            }
            else return base.OnPickup(item, player);
        }
    }
}
