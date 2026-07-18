using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.ModPlayers
{
    public class ParryBuffTracker : ModPlayer
    {
        public bool isStrikeReady = false;
        public override void PostUpdateBuffs()
        {
            isStrikeReady = Player.HasBuff(BuffID.ParryDamageBuff);
        }
    }
}
