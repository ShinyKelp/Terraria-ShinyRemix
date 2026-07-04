using ShinyRemix.SwordProjectileRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks.GlobalItems
{
    public class InfluxWaver : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileReworks && entity.type == ItemID.InfluxWaver;
        }
        public override void SetDefaults(Item entity)
        {
            base.SetDefaults(entity);
            entity.damage = 130;
        }
    }
}
