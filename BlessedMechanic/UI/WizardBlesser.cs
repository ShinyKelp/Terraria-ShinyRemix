using ShinyRemix.BlessedMechanic.GlobalItems;
using ShinyRemix.Common.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.UI
{
    public class WizardBlesser : ExtraButtonSystem
    {
        protected override int NPC_ID => NPCID.Wizard;
        protected override string ButtonText => "Bless";

        protected override void OnButtonPressed()
        {
            BlessHeldWeapon();
        }
        private void BlessHeldWeapon()
        {
            Player player = Main.LocalPlayer;
            Item item = player.HeldItem;

            if (item.IsAir)
            {
                Main.npcChatText = "Hold a magic weapon.";
                return;
            }

            if (item.DamageType != DamageClass.Magic)
            {
                Main.npcChatText = "This is not a magic weapon.";
                return;
            }

            var global = item.GetGlobalItem<BlessedItem>();

            if (global.blessed)
            {
                Main.npcChatText = "Already blessed.";
                return;
            }

            if (!player.BuyItem(Item.buyPrice(copper: player.HeldItem.value * 2)))
            {
                Main.npcChatText = "Not enough gold.";
                return;
            }

            global.blessed = true;

            SoundEngine.PlaySound(SoundID.Item4);

            Main.npcChatText = $"{item.Name} has been blessed!";
        }
    }
}
