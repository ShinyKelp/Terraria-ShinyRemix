using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.MagicPowerPot.ModPlayers
{
    public class MagicPowerTweaks : ModPlayer
    {
        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
            if(ShinyOptions.MagicPowerBoost && Player.HasBuff(BuffID.MagicPower))
            {
                healValue = (int)Math.Floor(healValue / 2f);
            }
        }
        public override void PostUpdateBuffs()
        {
            if (ShinyOptions.MagicPowerBoost && Player.HasBuff(BuffID.MagicPower))
            {
                Player.GetDamage(DamageClass.Magic) += 0.1f;
                Player.GetCritChance(DamageClass.Magic) += 5f;
            }
        }
    }
}
