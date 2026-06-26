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

namespace ShinyRemix.BiomeWeapons.GlobalItems
{
    public class RainbowGun : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ItemID.RainbowGun;
        }

        public override void SetDefaults(Item entity)
        {
            entity.shoot = ProjectileID.RainbowRodBullet;
            entity.channel = true;
            entity.mana = 3;
            entity.damage = 60;
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.whoAmI == Main.myPlayer)
            {
                Vector2 newVel = velocity.RotatedBy(MathHelper.PiOver4 * 0.08f * player.direction);
                Vector2 newVel2 = velocity.RotatedBy(MathHelper.PiOver4 * -0.05f * player.direction);
                Projectile.NewProjectile(source, position, newVel, ProjectileID.MagicMissile, damage, knockback, player.whoAmI, 0f, 1f, 0.55f);
                Projectile.NewProjectile(source, position, newVel2, ProjectileID.Flamelash, damage, knockback, player.whoAmI, 0f, 2f, 0.25f);
            }
            return true;
        }
    }
}
