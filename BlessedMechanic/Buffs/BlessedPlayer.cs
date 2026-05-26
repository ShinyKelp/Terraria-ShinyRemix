using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.Buffs
{
    public class BlessedPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<BlessedBuff>()))
            {
                Player.manaCost *= 0.2f;
            }
        }
    }
}
