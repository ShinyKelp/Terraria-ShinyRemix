using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Misc.GlobalItems
{
    public class Cutlass : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Cutlass;
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int num5 = Main.rand.Next(1, 4);
            num5 = 1;
            for (int j = 0; j < num5; j++)
            {
                Rectangle hitbox = target.Hitbox;
                hitbox.Inflate(30, 16);
                hitbox.Y -= 8;
                Vector2 vector3 = Main.rand.NextVector2FromRectangle(hitbox);
                Vector2 vector4 = hitbox.Center.ToVector2();
                Vector2 spinningpoint = (vector4 - vector3).SafeNormalize(new Vector2((float)player.direction, player.gravDir)) * 8f;
                Main.rand.NextFloat();
                float num6 = (float)(Main.rand.Next(2) * 2 - 1) * (0.62831855f + 2.5132742f * Main.rand.NextFloat());
                num6 *= 0.5f;
                spinningpoint = spinningpoint.RotatedBy(0.7853981852531433, default(Vector2));
                int num7 = 3;
                int num8 = 10 * num7;
                int num9 = 5;
                int num10 = num9 * num7;
                vector3 = vector4;
                for (int k = 0; k < num10; k++)
                {
                    vector3 -= spinningpoint;
                    spinningpoint = spinningpoint.RotatedBy((double)((0f - num6) / (float)num8), default(Vector2));
                }
                vector3 += target.velocity * (float)num9;
                Projectile.NewProjectile(player.GetSource_ItemUse(item), vector3, spinningpoint, 977, (int)((float)damageDone * 0.5f), 0f, player.whoAmI, num6, 0f, 0f);
            }
        }
    }
}
