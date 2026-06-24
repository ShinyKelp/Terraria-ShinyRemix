using ShinyRemix.SwordParries.ModBuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordParries.ModPlayers
{
    public class ParryBuffBuff : ModPlayer
    {
        bool hadParryBuff = false;
        bool hadParryExtensionBuff = false;
        public override void PostUpdateBuffs()
        {
            bool hasParryBuff = Player.HasBuff(BuffID.ParryDamageBuff);
            if (hadParryBuff && !hasParryBuff)
            {
                //Just lost the buff.
                SoundStyle style = SoundID.Item43 with
                {
                    Pitch = 0.6f,
                    Volume = 2.4f

                };
                SoundEngine.PlaySound(style, Player.position);
                Player.AddBuff(ModContent.BuffType<ParryStrikeExtension>(), 120);
            }
            else if (hadParryBuff && hasParryBuff && Player.buffTime.Length > BuffID.ParryDamageBuff && Player.buffTime[BuffID.ParryDamageBuff] == 1)
            {
                //Strike buff runs out naturally. Don't count it.
                hasParryBuff = false;
            }
            hadParryBuff = hasParryBuff;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (modifiers.DamageType == DamageClass.Melee && Player.HasBuff(ModContent.BuffType<ParryStrikeExtension>()))
                modifiers.SourceDamage += 1f;
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
