using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShinyRemix.OldManQuest.ModPlayers
{
    public class PlayerDarkItem : ModPlayer
    {
        public bool darkItemEquipped = false;
        public override void ResetEffects()
        {
            darkItemEquipped = false;
        }
    }
}
