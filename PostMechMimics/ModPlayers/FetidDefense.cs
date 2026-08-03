using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.ModPlayers
{
    public class FetidDefense : ModPlayer
    {
        public int defenseTimer = 0;
        public override void ResetEffects()
        {
            if (defenseTimer > 0)
                defenseTimer--;
        }

        public override void PostUpdateEquips()
        {
            if (defenseTimer > 0)
                Player.statDefense *= 1.1f;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            base.OnHurt(info);
        }
    }
}
