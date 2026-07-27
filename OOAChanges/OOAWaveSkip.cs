using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace ShinyRemix.OOAChanges
{
    public static class OOAWaveSkip
    {
        public static void OOAStartWaveSkip(On_DD2Event.orig_StartInvasion orig, int difficultyOverride)
        {
            orig(difficultyOverride);
            if (Main.netMode != NetmodeID.MultiplayerClient && ShinyOptions.OldOneArmyBuffs)
            {
                switch (DD2Event.OngoingDifficulty)
                {
                    case 1:
                        NPC.waveNumber = 3;
                        break;
                    case 2:
                        NPC.waveNumber = 5;
                        break;
                    case 3:
                        NPC.waveNumber = 4;
                        break;
                }
            }
        }
    }
}
