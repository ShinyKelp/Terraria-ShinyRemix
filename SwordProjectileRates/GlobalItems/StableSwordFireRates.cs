using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace ShinyRemix.SwordProjectileRates.GlobalItems
{
    public class StableSwordFireRates : GlobalItem
    {
        public override bool InstancePerEntity => true;
        protected int swingsPerShot = 2;
        public static int Swings { get; private set; } = 0;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.SwordProjectileRates && entity.DamageType == DamageClass.Melee && !entity.noMelee && entity.pick == 0 && entity.axe == 0 && entity.hammer == 0 && entity.useStyle == ItemUseStyleID.Swing && entity.shoot != -1 && !entity.shootsEveryUse;
        }

        public override void SetDefaults(Item entity)
        {
            entity.shootsEveryUse = true;
            if(SwordRateUtils.ModSwordRates.ContainsKey(entity.type))
                swingsPerShot = SwordRateUtils.ModSwordRates[entity.type];
            else
            {
                switch (entity.type)
                {
                    case ItemID.IceSickle:
                    case ItemID.DeathSickle:
                    case ItemID.LightsBane:
                    case ItemID.StarWrath:
                        swingsPerShot = 1;
                        break;
                    case ItemID.IceBlade:
                    case ItemID.Starfury:
                    case ItemID.ChlorophyteSaber:
                    case ItemID.BeamSword:
                        swingsPerShot = 3;
                        break;
                    case ItemID.Seedler:
                        swingsPerShot = 3;
                        entity.damage += 15;
                        break;
                    case ItemID.EnchantedSword:
                        swingsPerShot = 4;
                        break;
                    default:
                        swingsPerShot = 2;
                        break;
                }
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(type == item.shoot)
            {
                Swings++;
                if (Swings >= 0)
                {
                    Swings = -swingsPerShot;
                    return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                }
                else
                    return false;
            }
            else
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public static void ResetSwings()
        {
            Swings = 0;
        }
    }
}
