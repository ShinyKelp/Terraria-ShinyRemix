using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.ModPlayers
{
    public class OctopodReuseDelay : ModPlayer
    {
        public float trueReuseDelay = 0f;
        public bool nextReuseNerf = true;
        public override void PostUpdate()
        {
            if(!Player.dead && trueReuseDelay > 0f)
            {
                if(nextReuseNerf)
                    Player.reuseDelay = (int)Math.Ceiling(trueReuseDelay * 2f);
                else
                    Player.reuseDelay = (int)Math.Ceiling(trueReuseDelay / 2f);
                trueReuseDelay = 0;
                nextReuseNerf = true;
            }
        }
    }
}
