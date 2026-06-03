using Microsoft.Xna.Framework;
using ShinyRemix.SwordProjectileRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.FrostWeapons.GlobalItems
{
    public class FrostWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.FrostWeaponChanges &&
                (entity.type == ItemID.FrostStaff || entity.type == ItemID.Frostbrand || entity.type == ItemID.IceBlade);
        }
        public override void SetDefaults(Item entity)
        {
            if(entity.type == ItemID.FrostStaff)
            {
                entity.useTime = 24;
                entity.useAnimation = entity.useTime;
                entity.damage = 48;
            }
            else if(entity.type == ItemID.Frostbrand)
            {
                entity.useTime = 32;
                entity.useAnimation = entity.useTime;
                entity.damage = 54;
            }
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(type == ProjectileID.FrostBoltSword || type == ProjectileID.IceBolt)
            {
                float currentSpeed = velocity.Length();
                float newSpeed = MathHelper.Lerp(
                    currentSpeed,
                    type == ProjectileID.FrostBoltStaff ? 14f : 11f,
                    0.8f
                );
                velocity.Normalize();
                velocity *= newSpeed;
            }
           
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if((type == ProjectileID.FrostBoltSword) && Main.myPlayer == player.whoAmI)
            {
                if (StableSwordFireRates.Swings == -1 && Main.rand.NextFloat() <  0.4f)
                {
                    int initDir = Math.Sign(velocity.X);
                    Vector2 extraPos = position;
                    extraPos.X -= 8f * initDir;
                    extraPos.Y -= 8f;

                    Vector2 extraVel = velocity;
                    float randRotate = Main.rand.NextFloat() + 0.25f;
                    extraVel = extraVel.RotatedBy(-MathHelper.PiOver4 * randRotate * initDir);
                    extraVel *= 0.9f;

                    Projectile proj = Projectile.NewProjectileDirect(source, extraPos, extraVel, type, damage, knockback);
                    int randTime = 4 + Main.rand.Next(6);
                    proj.timeLeft -= randTime;
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
