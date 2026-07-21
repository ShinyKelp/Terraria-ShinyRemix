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

        protected override void OnButtonPressed()
        {
            Player player = Main.LocalPlayer;
            Item item = player.HeldItem;

            if (item.IsAir)
            {
                Main.npcChatText = "Hold a ranged weapon.";
                return;
            }

            if (item.DamageType != DamageClass.Ranged)
            {
                Main.npcChatText = "This is not a ranged weapon.";
                return;
            }

            var global = item.GetGlobalItem<OverloadedItem>();

            if (global.overloaded)
            {
                Main.npcChatText = "Already overloaded.";
                return;
            }

            if (!player.BuyItem(Item.buyPrice(copper: player.HeldItem.value * 2)))
            {
                Main.npcChatText = "Not enough gold.";
                return;
            }

            global.overloaded = true;

            SoundEngine.PlaySound(SoundID.Item4);

            Main.npcChatText = $"{item.Name} has been overloaded!";
        }
    }
}
