using Microsoft.Xna.Framework;
using ShinyRemix.SwordProjectileRates.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks.GlobalItems
{
    public class FrostSwords : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.Misc &&
                (entity.type == ItemID.Frostbrand || entity.type == ItemID.IceBlade);
        }


        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.FrostBoltSword || type == ProjectileID.IceBolt)
            {
                float currentSpeed = velocity.Length();
                float newSpeed = MathHelper.Lerp(
                    currentSpeed,
                    type == ProjectileID.FrostBoltSword ? 14f : 8f,
                    0.8f
                );
                velocity.Normalize();
                velocity *= newSpeed;
            }
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if ((type == ProjectileID.FrostBoltSword) && Main.myPlayer == player.whoAmI)
            {
                if (StableSwordFireRates.Swings == -2 && Main.rand.NextFloat() < 0.4f)
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
