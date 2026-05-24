using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Common
{
    public class TRAEGlove : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyUtils.TRAE && entity.type == ItemID.TitanGlove || entity.type == ItemID.PowerGlove || entity.type == ItemID.BerserkerGlove;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.GetModPlayer<ShinyMeleeScale>().meleeScaleGlove = true;
        }

    }
    public class ShinyMeleeScale : ModPlayer
    {
        public bool meleeScaleGlove = false;

        public override void ResetEffects()
        {
            meleeScaleGlove = false;
        }
    }
}
