using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShinyRemix.OverloadMechanic.GlobalItems
{
    public class OverloadedItem : GlobalItem
    {
        public bool overloaded = false;
        public int lastShotFrames = 0;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.OverloadMechanic && entity.DamageType == DamageClass.Ranged && entity.useAmmo != AmmoID.None;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            if (overloaded)
                tag["overloaded"] = true;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            overloaded = tag.GetBool("overloaded");
        }

        public override void UpdateInventory(Item item, Player player)
        {
            lastShotFrames++;
            base.UpdateInventory(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (overloaded)
            {
                TooltipLine line = new TooltipLine(Mod, "Overloaded", "Overloaded: inverts ammo reservation for more damage.");
                line.OverrideColor = Color.MediumPurple;
                tooltips.Add(line);
            }
        }
    }
}
