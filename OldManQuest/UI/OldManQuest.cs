using ShinyRemix.Common.UI;
using ShinyRemix.OldManQuest.ModPlayers;
using ShinyRemix.OldManQuest.ModSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.CursedManQuest.UI
{
    public class OldManQuest : ExtraButtonSystem
    {
        protected override int NPC_ID => NPCID.OldMan;
        protected override string ButtonText => "Aid";
        protected override bool CustomLogicCheck()
        {
            return ShinyOptions.OldManQuest;
        }
        protected override void OnButtonPressed()
        {
            Player player = Main.player[Main.myPlayer];
            if (QuestCompletion.CompletedPlayers.Contains(player.name))
            {
                Main.npcChatText = "There's nothing else I can do for you. Please, free me from my master!";
            }
            else
            {
                if (player.GetModPlayer<PlayerDarkItem>().darkItemEquipped)
                {

                    DamageClass weaponType = CheckPlayerWeapon(player);
                    int spawnID = -1;
                    if (weaponType == DamageClass.Melee || weaponType == DamageClass.MeleeNoSpeed)
                        spawnID = ItemID.Muramasa;
                    else if (weaponType == DamageClass.Ranged)
                        spawnID = ItemID.Handgun;
                    else if (weaponType == DamageClass.Magic)
                        spawnID = ItemID.WaterBolt;

                    if(spawnID == -1)
                    {
                        Main.npcChatText = "If you want my help, show me your weapon of choice.";
                    }
                    else
                    {
                        Main.npcChatText = "You seem to have an affinity with the shadows… Perhaps I can assist you yet.";
                        int itemID = player.QuickSpawnItem(player.GetSource_GiftOrReward(), spawnID);
                        Item spawnedItem = Main.item[itemID];
                        Item baseItem = new Item();
                        baseItem.SetDefaults(spawnedItem.type);
                        int rollAttemts = 50;
                        while (spawnedItem.value >= baseItem.value && rollAttemts > 0)
                        {
                            spawnedItem.SetDefaults(spawnedItem.type);
                            if (!spawnedItem.Prefix(-1))
                            {
                                Main.NewText($"Warning: Prefix did not work.");
                            }
                            else
                                Main.NewText($"Prefix value: {baseItem.value} - {spawnedItem.value}");
                            rollAttemts--;
                        }
                        if (rollAttemts == 0)
                        {
                            spawnedItem.Prefix(0);
                            spawnedItem.SetDefaults(spawnedItem.type);
                        }
                        QuestCompletion.CompletedPlayers.Add(player.name);
                    }
                }
                else 
                {
                    Main.npcChatText = "I can't help you in your current state. Commune with the beings of the shadows, and pray you come back to me alive.";
                }
            }
        }
        private DamageClass CheckPlayerWeapon(Player player)
        {
            if(!player.HeldItem.IsAir && player.HeldItem.damage > 0)
            {
                return player.HeldItem.DamageType;
            }
            else
                return DamageClass.Default;
        }
    }
}
