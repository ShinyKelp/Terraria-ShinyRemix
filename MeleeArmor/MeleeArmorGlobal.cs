using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.MeleeArmor
{
    public class MeleeArmorGlobal : GlobalItem
    {

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.MeleeArmorChanges;
        }

        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                //Molten. 25 > 22.
                case ItemID.MoltenHelmet:
                    item.defense -= 1;
                    break;

                case ItemID.MoltenBreastplate:
                    item.defense -= 1;
                    break;

                case ItemID.MoltenGreaves:
                    item.defense -= 1;
                    break;

                //Hardmode melee head pieces
                case ItemID.CobaltMask:
                    item.defense -= 5; //14 > 9
                    break;

                case ItemID.PalladiumMask:
                    item.defense -= 5; //14 > 9
                    break;

                case ItemID.MythrilHelmet:
                    item.defense -= 5; //16 > 11
                    break;

                case ItemID.OrichalcumMask:
                    item.defense -= 7; //19 > 12
                    break;

                case ItemID.AdamantiteMask:
                    item.defense -= 7; //22 > 15
                    break;

                case ItemID.TitaniumMask:
                    item.defense -= 7; //23 > 16
                    break;

                case ItemID.HallowedMask:
                    item.defense -= 6; //24 > 18
                    break;

                case ItemID.ChlorophyteMask:
                    item.defense -= 3; //20 > 17
                    break;

                //Turtle armor (65 > 54)
                case ItemID.TurtleHelmet:
                    item.defense -= 3;  //21 > 18
                    break;

                case ItemID.TurtleScaleMail:
                    item.defense -= 5;  //27 > 22
                    break;

                case ItemID.TurtleLeggings:
                    item.defense -= 3;  //17 > 14
                    break;

                //Beetle armor (Scalemail: 61 > 54. Shell: 73 > 60)
                case ItemID.BeetleHelmet:
                    item.defense -= 3;  //23 > 20
                    break;

                case ItemID.BeetleLeggings:
                    item.defense -= 2;  //18 > 16
                    break;

                case ItemID.BeetleScaleMail:
                    item.defense -= 2; //20 > 18
                    break;

                case ItemID.BeetleShell:
                    item.defense -= 8;  //32 > 24
                    break;

                //Solar armor
                case ItemID.SolarFlareBreastplate:
                    item.defense -= 6;  //34 > 28
                    break;

            }
        }
        public override void UpdateEquip(Item item, Player player)
        {
            switch (item.type)
            {
                //Molten.
                case ItemID.MoltenHelmet:
                    player.GetCritChance<MeleeDamageClass>() += 0.05f;  //7 >12%
                    break;

                case ItemID.MoltenBreastplate:
                    player.GetDamage<MeleeDamageClass>() += 0.05f;  //7 > 12%
                    break;

                //Hardmode melee head pieces
                case ItemID.CobaltMask:
                    player.GetCritChance<MeleeDamageClass>() += 0.1f;   //0 > 10%
                    break;

                case ItemID.PalladiumMask:
                    player.GetCritChance<MeleeDamageClass>() += 0.09f;  //0 > 9%
                    break;

                case ItemID.MythrilHelmet:
                    player.GetDamage<MeleeDamageClass>() += 0.06f;  //10 > 16%
                    break;

                case ItemID.OrichalcumMask:
                    player.GetCritChance<MeleeDamageClass>() += 0.07f;  //0 > 7%
                    break;

                case ItemID.AdamantiteMask:
                    player.GetDamage<MeleeDamageClass>() += 0.04f;  //14 > 18%
                    player.GetCritChance<MeleeDamageClass>() += 0.07f;  //7 > 14%
                    break;

                case ItemID.TitaniumMask:
                    player.GetDamage<MeleeDamageClass>() += 0.07f;  //9 > 16%
                    break;

                case ItemID.HallowedMask:
                    player.GetDamage<MeleeDamageClass>() += 0.05f;  //10 > 15%
                    player.GetCritChance<MeleeDamageClass>() += 0.05f; //10 > 15%
                    break;

                case ItemID.ChlorophyteMask:
                    player.GetDamage<MeleeDamageClass>() += 0.02f;  //16 > 18%
                    player.GetCritChance<MeleeDamageClass>() += 0.06f;  //6 > 12%
                    break;

                //Turtle armor (65 > 54)
                case ItemID.TurtleHelmet:
                    player.GetDamage<MeleeDamageClass>() += 0.02f;  //6 > 8%
                    break;

                case ItemID.TurtleScaleMail:
                    player.GetDamage<MeleeDamageClass>() += 0.02f;  //8 > 10%
                    player.GetCritChance<MeleeDamageClass>() += 0.02f;  //8 > 10%
                    break;

                case ItemID.TurtleLeggings:
                    player.GetCritChance<MeleeDamageClass>() += 0.02f;  //4 > 6%
                    break;

                //Beetle armor (Scalemail: 61 > 54. Shell: 73 > 60)
                case ItemID.BeetleHelmet:
                    player.GetDamage<MeleeDamageClass>() += 0.03f;  //6 > 9%
                    break;

                case ItemID.BeetleScaleMail:
                    player.GetDamage<MeleeDamageClass>() += 0.03f;  //8 > 11%
                    break;

                //Solar armor
                case ItemID.SolarFlareBreastplate:
                    player.GetDamage<MeleeDamageClass>() += 0.07f;  //29 > 36%
                    break;
            }
        }
    }
}
