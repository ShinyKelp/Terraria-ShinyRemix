using ShinyRemix.BlessedMechanic.GlobalItems;
using ShinyRemix.Common.UI;
using ShinyRemix.OverloadMechanic.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OverloadMechanic.UI
{
    public class DealerHijacker : ExtraButtonSystem
    {
        protected override int NPC_ID => NPCID.ArmsDealer;
        protected override string ButtonText => "Overload";

        protected override bool CustomLogicCheck()
        {
            return ShinyOptions.OverloadMechanic;
        }
    }
}
