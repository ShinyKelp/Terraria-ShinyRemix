using ShinyRemix.LifestealRework.ModBuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.LifestealRework.ModPlayers
{
    public class LifestealTweaks : ModPlayer
    {
        const float TRAEMaxLifeSteal = 4f;
        const float MaxLifeSteal = 60f;
        const float targetLifeStealPerTick = 0.25f;
        float previousLifeSteal = -1;
        public override void PostUpdate()
        {
            if (ShinyOptions.LifestealRework)
            {
                if (previousLifeSteal > 0)
                {
                    float lifeGained = previousLifeSteal - Player.lifeSteal;
                    if(lifeGained > 0.3f)
                    {
                        lifeGained -= 0.3f;
                        Player.statLife = (int)Math.Max(1, Player.statLife - (int)Math.Floor(lifeGained));
                    }
                }
                if (!ShinyUtils.TRAE)
                {
                    if (Player.lifeSteal > MaxLifeSteal)
                        Player.lifeSteal = MaxLifeSteal;
                    if (Player.lifeSteal < MaxLifeSteal)
                        Player.lifeSteal += (targetLifeStealPerTick - 0.5f);   //Counteracting vanilla's +0.5f per tick.
                }
                previousLifeSteal = Player.lifeSteal;
            }
        }

        public override void PreUpdateBuffs()
        {
            if (!ShinyOptions.LifestealRework)
                return;
            if(Player.lifeSteal < 0f)
            {
                if (!ShinyUtils.TRAE && !Player.HasBuff(ModContent.BuffType<LifestealBuff>()))
                {
                    SoundStyle style = SoundID.Item92 with
                    {
                        Pitch = 0.35f,
                        Volume = 1.1f

                    };
                    SoundEngine.PlaySound(SoundID.Item92);
                }
                if (ShinyUtils.TRAE)
                    Player.AddBuff(ModContent.BuffType<LifestealBuff>(), 300);
                else
                    Player.AddBuff(ModContent.BuffType<LifestealBuff>(), 600);
            }
        }

        public override void UpdateLifeRegen()
        {
            if (!ShinyOptions.LifestealRework)
                return;
            if (Player.HasBuff(ModContent.BuffType<LifestealBuff>()))
            {
                if (ShinyUtils.TRAE)
                    Player.lifeRegen += 8;
                else
                    Player.lifeRegen += 16;
            }
        }
    }
}
