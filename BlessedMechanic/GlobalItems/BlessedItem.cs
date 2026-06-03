using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShinyRemix.BlessedMechanic.GlobalItems
{
    public class BlessedItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool blessed = false;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BlessedMechanic && entity.DamageType == DamageClass.Magic;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            if (blessed)
                tag["blessed"] = true;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            blessed = tag.GetBool("blessed");
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (blessed)
            {
                damage *= 0.5f;
            }
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            if (blessed && item.DamageType == DamageClass.Magic)
            {
                mult = 0f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (blessed)
            {
                TooltipLine line = new TooltipLine(Mod, "Blessed", "Blessed: deals half damage but consumes no mana.");
                line.OverrideColor = Color.MediumPurple;
                tooltips.Add(line);
            }
        }

    }
}
