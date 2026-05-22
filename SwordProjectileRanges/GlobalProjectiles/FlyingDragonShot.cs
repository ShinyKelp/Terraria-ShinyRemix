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

namespace ShinyRemix.SwordProjectileRanges.GlobalProjectiles
{
    public class FlyingDragonShot : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.DD2SquireSonicBoom;
        }

        Vector2 direction;
        Player player;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            direction = projectile.velocity.SafeNormalize(Vector2.UnitX);
            projectile.timeLeft = 25;
            player = Main.player[projectile.owner];

            projectile.scale = player.HeldItem.scale;
            if (player.meleeScaleGlove)
                projectile.scale += 0.1f;
        }

        public override void AI(Projectile projectile)
        {
            projectile.velocity = direction * 24f;
        }

    }
}
