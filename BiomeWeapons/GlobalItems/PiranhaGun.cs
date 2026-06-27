using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalItems
{
    internal class PiranhaGun : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ItemID.PiranhaGun;
        }
        public override void SetDefaults(Item entity)
        {
            entity.damage = 36; // Should change to -4 dmg after 1.4.5 buff.
        }
    }
}
