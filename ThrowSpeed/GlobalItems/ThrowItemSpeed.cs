using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.ThrowSpeed.GlobalItems
{
    public class ThrowItemSpeed : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.ThrowSpeedScaling && ThrowSpeedUtils.ThrowItems.Contains(entity.type);
        }

        public override void SetDefaults(Item entity)
        {
            entity.DamageType = DamageClass.Melee;
            if (entity.useTime != entity.useAnimation)
                return;
            entity.useTime += (int)Math.Floor(entity.useTime * 0.1f) + 2;
            if (entity.type == ItemID.DayBreak) 
            { 
                if (!(ShinyUtils.TRAE && NNBSpearUtils.ModSpearIDs["Daybreak"] != -1))
                {
                    entity.damage -= 10;
                }
            }
            entity.useAnimation = entity.useTime;
        }
    }
}
