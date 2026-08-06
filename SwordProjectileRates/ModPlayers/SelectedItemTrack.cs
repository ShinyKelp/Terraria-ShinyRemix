using ShinyRemix.SwordProjectileRates.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileRates.ModPlayers
{
    public class SelectedItemTrack : ModPlayer
    {
        private int previousSelectedItem = -1;

        public override void PostUpdate()
        {
            int current = Player.selectedItem;

            if (current != previousSelectedItem)
            {
                previousSelectedItem = current;
                StableSwordFireRates.ResetSwings();
            }
        }
    }
}
