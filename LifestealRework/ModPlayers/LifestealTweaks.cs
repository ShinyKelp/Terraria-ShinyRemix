using ShinyRemix.LifestealRework.ModBuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.LifestealRework.ModPlayers
{
    public class LifestealTweaks : ModPlayer
    {
        const float baseMaxLifesteal = 40f;
        float MaxLifeSteal => (Main.expertMode ? baseMaxLifesteal : baseMaxLifesteal + 20f);
        const float targetLifestealPerTick = 0.1f;

        public override void PostUpdate()
        {

            if(Player.lifeSteal > MaxLifeSteal)
                Player.lifeSteal = MaxLifeSteal;
            if (Player.lifeSteal < MaxLifeSteal)
                Player.lifeSteal += (targetLifestealPerTick - 0.5f);   //Counteracting vanilla's +0.5f per tick.
        }

        public override void PreUpdateBuffs()
        {
            if (Player.lifeSteal < MaxLifeSteal)
                Player.AddBuff(ModContent.BuffType<LifestealBuff>(), 240);
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<LifestealBuff>()))
                Player.lifeRegen += 8;
        }
    }
}
