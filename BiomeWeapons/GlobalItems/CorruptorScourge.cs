using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalItems
{
    public class CorruptorScourge : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ItemID.ScourgeoftheCorruptor;
        }
        public override void SetStaticDefaults()
        {
            PrefixLegacy.ItemSets.SwordsHammersAxesPicks[ItemID.ScourgeoftheCorruptor] = true;
        }
        public override void SetDefaults(Item entity)
        {
            entity.DamageType = DamageClass.Melee;
            entity.useTime = entity.useAnimation = 26;
            if (ShinyUtils.TRAE)
            {
                entity.useTime = entity.useAnimation = 33;
                entity.damage -= 5;
            }
            PrefixLegacy.ItemSets.SwordsHammersAxesPicks[entity.type] = true;
        }
        public override bool AllowPrefix(Item item, int pre)
        {
            ModPrefix prefix = PrefixLoader.GetPrefix(pre);
            if (prefix == null)
            {
                return true;
            }
            float damageMult, knockbackMult, useTimeMult, scaleMult, shotSpeedMult, manaUseMult;
            damageMult = knockbackMult = useTimeMult = scaleMult = shotSpeedMult = manaUseMult = 1f;
            int critBonus = 4;
            prefix.SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shotSpeedMult, ref manaUseMult, ref critBonus);
            return useTimeMult == 1f;
        }
    }
}
