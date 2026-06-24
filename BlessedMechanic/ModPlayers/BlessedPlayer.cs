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
        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff(ModContent.BuffType<BlessedBuff>()))
            {
                Player.manaCost *= 0.2f;
                Player.manaRegenBuff = true;
                if (ShinyUtils.TRAE)
                {
                    customManaRegen++;
                    if(customManaRegen >= 5)
                    {
                        customManaRegen = 0;
                        Player.statMana = Math.Min(Player.statMana + 1, Player.statManaMax2);
                    }
                    //Kind of ugly compatibility with TRAE here, just a flat 12 mana per second.
                }
            }
        }
    }
}
