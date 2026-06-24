using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordParries.GlobalItems
{
    public class ParrySwords : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool IsParrySword = false;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordParries && 
                ((entity.DamageType == DamageClass.Melee && !entity.noMelee && entity.pick == 0 && entity.axe == 0 && entity.hammer == 0 && entity.useStyle == ItemUseStyleID.Swing)
                || SwordParryUtils.AdditionalSwords.Contains(entity.type));
        }
        public override void SetDefaults(Item entity)
        {
            IsParrySword = true;
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.HasBuff(BuffID.ParryDamageBuff))
                Main.NewText($"Detected buff in modify hit!");
            base.ModifyHitNPC(item, player, target, ref modifiers);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.HasBuff(BuffID.ParryDamageBuff))
                Main.NewText($"Detected buff in on hit!");
            base.OnHitNPC(item, player, target, hit, damageDone);
        }
    }
}
