using ShinyRemix.BlessedMechanic.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.ModPlayers
{
    public class BlessedPlayer : ModPlayer
    {
        private int customManaRegen = 0;
        private int FramesForNextTick = 1;
        private float leftover = 0f;
        void CalculateNextManaCountdown()
        {
            float decimalCount = 60f / (Player.statManaMax2 * 0.1f) + leftover;
            FramesForNextTick = (int)decimalCount;
            leftover = decimalCount - (float)Math.Floor(decimalCount);
        }
        private int FramesPerManaTick => (int)Math.Round(60f / (Player.statManaMax2 * 0.1f));
        public override void PostUpdateBuffs()
        {
            if (Player.whoAmI == Main.myPlayer && Player.HasBuff(ModContent.BuffType<BlessedBuff>()))
            {
                Player.manaCost *= 0.2f;
                Player.manaRegenBuff = true;
                customManaRegen++;
                if(customManaRegen >= FramesForNextTick)
                {
                    customManaRegen = 0;
                    Player.statMana = Math.Min(Player.statMana + 1, Player.statManaMax2);
                    CalculateNextManaCountdown();
                }
            }
        }
    }
}
