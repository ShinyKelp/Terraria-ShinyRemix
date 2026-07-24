using Microsoft.Xna.Framework;
using ShinyRemix.SwordProjectileRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PreMechMage.GlobalItems
{
    public class FrostWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PreMechMage &&
                entity.type == ItemID.FrostStaff;
        }
        public override void SetDefaults(Item entity)
        {
            entity.useTime = entity.useAnimation = 24;
            entity.damage += 4;
            entity.mana += 8;
        }
    }
}
