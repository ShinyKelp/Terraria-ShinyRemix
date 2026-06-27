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
    internal class VampireKnives : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ItemID.VampireKnives;
        }
        public override void SetDefaults(Item entity)
        {
            entity.useTime = entity.useAnimation = 22;
            entity.DamageType = DamageClass.Melee;
        }

        public override void SetStaticDefaults()
        {
            if(ShinyOptions.BiomeKeyWeapons)
                PrefixLegacy.ItemSets.SwordsHammersAxesPicks[ItemID.VampireKnives] = true;
        }
        public override bool AllowPrefix(Item item, int pre)
        {
            if (ShinyUtils.SizePrefixes.Contains(pre))
                return false;
            ModPrefix prefix = PrefixLoader.GetPrefix(pre);
            if (prefix == null)
                return true;
            float damageMult, knockbackMult, useTimeMult, scaleMult, shotSpeedMult, manaUseMult;
            damageMult = knockbackMult = useTimeMult = scaleMult = shotSpeedMult = manaUseMult = 1f;
            int critBonus = 4;
            prefix.SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shotSpeedMult, ref manaUseMult, ref critBonus);
            return scaleMult == 1f;
        }
    }
}
