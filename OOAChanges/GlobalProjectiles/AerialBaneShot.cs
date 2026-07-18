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
            if (projectile.owner == Main.myPlayer && projectile.ai[0] > 15 && canShootArrows && projectile.velocity.Y < 0 &&
                projectile.ai[0] > lastShot + shotCooldown)
            {
                Vector2 velocity = projectile.velocity;
                velocity.Y *= 0.4f;
                velocity.X *= 0.8f;
                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(),
                    projectile.Center,
                    velocity,
                    ProjectileID.DD2BetsyArrow,
                    projectile.damage,
                    projectile.knockBack,
                    projectile.owner,
                    projectile.ai[0],
                    -1f);
                hasShotArrow = true;
                lastShot = projectile.ai[0];
            }

            if (projectile.owner == Main.myPlayer && shotUpwards && projectile.ai[1] != -1 && projectile.velocity.Y > 0 && shotArrows < totalArrows)
            {
                //Shoot extra arrows
                Vector2 velocity = Vector2.UnitY * 6f;

                float totalSpread = MathHelper.PiOver4;
                float baseAngleOffset = MathHelper.PiOver4 * 0.6f;

                float angle = (baseAngleOffset + MathHelper.Lerp(-totalSpread / 2f, totalSpread / 2f, (shotArrows / totalArrows - 1))) * Math.Sign(projectile.velocity.X);

                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(),
                    projectile.Center,
                    velocity.RotatedBy(angle),
                    ProjectileID.DD2BetsyArrow,
                    projectile.damage,
                    projectile.knockBack,
                    projectile.owner,
                    0f,
                    -1f);

                shotArrows++;
                Main.NewText($"Shot arrows: {shotArrows}");

            }
            return true;
        }

    }
}
