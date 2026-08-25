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
                Main.npcChatText = "If you show me a ranged weapon, I can make it shoot more, for a price.";
                return;
            }

            if (item.DamageType != DamageClass.Ranged)
            {
                Main.npcChatText = "This is not a ranged weapon. I can't work with this.";
                return;
            }

            var global = item.GetGlobalItem<OverloadedItem>();

            if (global.overloaded)
            {
                Main.npcChatText = "This weapon has already been modified. If you want it restored, I'd look for a tinkerer.";
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
                        price = price / 100;
                        int goldPrice = price % 100;
                        if (goldPrice > 0)
                            moneyStr = " " + goldPrice + " gold" + moneyStr;
                        if (price >= 100)
                        {
                            price = price / 100;
                            moneyStr = " " + price + " platinum" + moneyStr;
                        }
                    }
                }
                Main.npcChatText = "You'll need to pay for that, this isn't a charity. This weapon will cost you" + moneyStr + ".";
                return;
            }

            global.overloaded = true;

            SoundEngine.PlaySound(SoundID.Item4);

            Main.npcChatText = $"I've hijacked {item.Name}. It should shoot more stuff now, but be careful not to run out of ammo.";
        }
    }
}
