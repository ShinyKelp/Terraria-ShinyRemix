using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalItems
{
    public class SpearItems : GlobalItem
    {
        //Add autoswing to all spears.
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return ShinyOptions.SpearRework && (NNBSpearUtils.VanillaSpears.Contains(item.type) ||
                NNBSpearUtils.ModSpearIDs.ContainsValue(item.type));
        }

        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
        }

        //Add size and speed reforges for vanilla spears
        public override void SetStaticDefaults()
        {
            for (int i = 0; i < NNBSpearUtils.VanillaSpears.Count; i++)
            {
                PrefixLegacy.ItemSets.SwordsHammersAxesPicks[NNBSpearUtils.VanillaSpears[i]] = true;
            }
        }

        //New AI disproportionately benefits attack speed, so we make spears scale slightly less off melee speed.
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            float vanillaSpeed = player.GetAttackSpeed(DamageClass.Melee);

            float meleeSpeedCompensation = MathHelper.Lerp(1f, vanillaSpeed, 0.3f);

            return 1f / meleeSpeedCompensation;
        }
    }
}
