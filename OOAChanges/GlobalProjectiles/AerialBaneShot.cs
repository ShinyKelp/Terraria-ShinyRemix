using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.GlobalProjectiles
{
    public class AerialBaneShot : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool shotUpwards = false;
        public int shotArrows = 0;
        public const int totalArrows = 3;

        public bool hasShotArrow = false;
        public int shotCooldown = 5;
        public float lastShot = 0;
        public bool canShootArrows = false;
        private IEntitySource cachedSource;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.DD2BetsyArrow;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.ai[1] != -1)
                canShootArrows = true;
            projectile.ai[1] = -1;
            cachedSource = source;
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.owner == Main.myPlayer && canShootArrows && projectile.velocity.Y < 0)
            {
                if(projectile.ai[0] == 25)
                {
                    Vector2 velocity = Vector2.Lerp(projectile.velocity, Vector2.UnitY, 0.025f);
                    Projectile.NewProjectile(
                        cachedSource,
                        projectile.Center,
                        velocity,
                        ProjectileID.DD2BetsyArrow,
                        projectile.damage,
                        projectile.knockBack,
                        projectile.owner,
                        projectile.ai[0],
                        -1f);
                }
            }

            return true;
        }

    }
}
