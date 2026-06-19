using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.ModPlayers
{
    public class StrongerLifeDrain : ModPlayer
    {
        public override void UpdateLifeRegen()
        {
            if (ShinyOptions.PostMechMimics && Player.HasBuff(BuffID.SoulDrain))
            {
                Player.lifeRegen += 6;
                Player.lifeRegenTime = 3600;
            }   
        }
    }
}
