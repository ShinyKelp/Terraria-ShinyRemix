using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PirateInvasionBuffs.GlobalItems
{
    public class CoinGun : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CoinGun;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            return Main.rand.NextFloat() < 0.8f;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "AmmoRes", "20% chance not to consume ammo.");
            tooltips.Add(line);
            base.ModifyTooltips(item, tooltips);
        }
    }
}
