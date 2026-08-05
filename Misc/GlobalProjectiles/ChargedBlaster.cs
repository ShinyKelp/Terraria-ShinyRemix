using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Misc.GlobalProjectiles
{
    public class ChargedBlaster : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return true && entity.type == ProjectileID.ChargedBlasterCannon;
        }
        public override bool PreAI(Projectile projectile)
        {

            if (projectile.ai[0] > 180f && projectile.ai[0] % 5 != 0)
            {
                Player player = Main.player[projectile.owner];

                Vector2 origin = player.RotatedRelativePoint(player.MountedCenter);

                Vector2 mouseWorld = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);

                if (player.gravDir == -1f)
                    mouseWorld.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;

                Vector2 velocity = mouseWorld - origin;
                velocity.Normalize();
                velocity *= player.HeldItem.shootSpeed * projectile.scale;

                projectile.velocity = velocity;
            }
            return base.PreAI(projectile);
        }
    }
}
