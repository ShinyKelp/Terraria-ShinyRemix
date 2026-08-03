using Microsoft.Xna.Framework;
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
            return true && ThrowSpeedUtils.ThrowItems.Contains(entity.type);
        }

        public override void SetDefaults(Item entity)
        {
            entity.DamageType = DamageClass.Melee;
            entity.useTime += (int)Math.Floor(entity.useTime * 0.2f);
            switch (entity.type)
            {
                case ItemID.VampireKnives:
                    entity.useTime += 2;
                    break;
                case ItemID.ShadowFlameKnife:
                case ItemID.DayBreak:
                    entity.useTime += 6;
                    break;
            }
            entity.useAnimation = entity.useTime;
        }

        public override float UseSpeedMultiplier(Item item, Player player)
        {
            float vanillaSpeed = player.GetAttackSpeed(DamageClass.Melee);

            float meleeSpeedCompensation = MathHelper.Lerp(1f, vanillaSpeed, 0.3f);

            return 1f / meleeSpeedCompensation;
        }
    }
}
