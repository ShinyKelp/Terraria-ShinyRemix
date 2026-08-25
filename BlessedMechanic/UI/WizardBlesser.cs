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
        protected override bool CustomLogicCheck()
        {
            return ShinyOptions.BlessedMechanic;
        }
        protected override void OnButtonPressed()
        {
            Player player = Main.LocalPlayer;
            Item item = player.HeldItem;

            if (item.IsAir)
            {
                Main.npcChatText = "Do you have a magical gadget? I can give it my blessing to it costs you no mana. Sounds fun right, lad?";
                return;
            }

            if (item.DamageType != DamageClass.Magic)
            {
                Main.npcChatText = "Hoho! Sorry, but I can't work with that!";
                return;
            }

            var global = item.GetGlobalItem<BlessedItem>();

            if (global.blessed)
            {
                Main.npcChatText = "Hm, this weapon already has a blessing. Do you want it removed? I would talk to the goblin if so.";
                return;
            }

            if (!player.BuyItem(Item.buyPrice(copper: player.HeldItem.value * 2)))
            {
                string moneyStr = "";
                int price = Item.buyPrice(copper: player.HeldItem.value * 2);
                int copperPrice = price % 100;
                if (copperPrice > 0)
                    moneyStr = " " + copperPrice + " copper";
                if (price >= 100)
                {
                    price = price / 100;
                    int silverPrice = price % 100;
                    if (silverPrice > 0)
                        moneyStr = " " + silverPrice + " silver" + moneyStr;
                    if (price >= 100)
                    {
                        Main.NewText($"Reached gold: {price}");
                        price = price / 100;
                        int goldPrice = price % 100;
                        if (goldPrice > 0)
                            moneyStr = " " + goldPrice + " gold" + moneyStr;
                        if (price >= 100)
                        {
                            Main.NewText($"Reached plat: {price}");
                            price = price / 100;
                            moneyStr = " " + price + " platinum" + moneyStr;
                        }
                    }
                }
                return;
            }

            global.blessed = true;

            SoundEngine.PlaySound(SoundID.Item4);

            Main.npcChatText = $"{item.Name} has my blessing. Enjoy it!";
        }
    }
}
