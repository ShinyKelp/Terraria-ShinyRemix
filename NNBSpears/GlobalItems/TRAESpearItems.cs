using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalItems
{
    public class TRAESpearItems : GlobalItem
    {

        static int[] TRAEVanillaSpears = new int[] { ItemID.DayBreak, ItemID.ScourgeoftheCorruptor, ItemID.Javelin, ItemID.BoneJavelin};

        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return NNBSpearUtils.TRAE && (NNBSpearUtils.VanillaSpears.Contains(item.type) ||
                TRAEVanillaSpears.Contains(item.type) ||
                item.type == NNBSpearUtils.TRAEJoterTridentItemID);
        }

        public override void SetDefaults(Item item)
        {
            item.UseSound = null;
            switch (item.type)
            {
                case ItemID.Spear:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.Spear;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.TheRottedFork:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.TheRottedFork;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.ThunderSpear:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.ThunderSpear;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.Trident:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.Trident;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.DarkLance:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.DarkLance;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.Swordfish:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.TheRottedFork;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.ObsidianSwordfish:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.ObsidianSwordfish;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.CobaltNaginata:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.CobaltNaginata;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.PalladiumPike:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.PalladiumPike;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.MythrilHalberd:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.MythrilHalberd;
                    item.DamageType = DamageClass.Melee;
                    item.channel = false;
                    break;

                case ItemID.OrichalcumHalberd:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.OrichalcumHalberd;
                    item.DamageType = DamageClass.Melee;
                    item.channel = false;
                    item.SetNameOverride("Orichalcum Billhook");
                    break;

                case ItemID.AdamantiteGlaive:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.AdamantiteGlaive;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.TitaniumTrident:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.TitaniumTrident;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.Gungnir:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.Gungnir;
                    item.DamageType = DamageClass.Melee;
                    item.useTime = item.useAnimation = 22;
                    break;

                case ItemID.ChlorophytePartisan:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.ChlorophytePartisan;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.MonkStaffT2:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.MonkStaffT2;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.MushroomSpear:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.MushroomSpear;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.NorthPole:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shoot = ProjectileID.NorthPoleWeapon;
                    item.DamageType = DamageClass.Melee;
                    break;

                        
                case ItemID.Javelin:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.DamageType = DamageClass.Melee;
                    break;
                case ItemID.BoneJavelin:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.DayBreak:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.DamageType = DamageClass.Melee;
                    break;

                case ItemID.ScourgeoftheCorruptor:
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.DamageType = DamageClass.Melee;
                    break;
            }
            if (item.type == NNBSpearUtils.TRAEJoterTridentItemID)
            {
                item.useStyle = ItemUseStyleID.Shoot;
                item.DamageType = DamageClass.Melee;
            }
            
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.altFunctionUse == 2) // right-click
            {
                item.useStyle = ItemUseStyleID.Swing;
            }
            else // left-click
            {
                item.useStyle = ItemUseStyleID.Shoot;
            }
            return base.CanUseItem(item, player);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse != 2 && item.type == ItemID.Trident)
            {
                type = ProjectileID.Trident; //Why is the Trident specifically re-assigned here? Idk, ask them.
            }
        }


    }
}
