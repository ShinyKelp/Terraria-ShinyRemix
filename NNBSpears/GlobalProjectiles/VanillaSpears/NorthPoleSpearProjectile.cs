using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class NorthPoleSpearProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SpearRework && entity.type == ProjectileID.NorthPoleSpear;
        }

        public override bool InstancePerEntity => true;

        private int framesPerSnowflake = 3;
        private bool firstFrame = true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.timeLeft = 26;
            projectile.penetrate = -1;
            base.OnSpawn(projectile, source);
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.localAI[0] == 0)
                projectile.localAI[0] = 8 - framesPerSnowflake;
            return true;
        }

        public override void PostAI(Projectile projectile)
        {
            if (firstFrame)
            {
                firstFrame = false;
                projectile.localAI[0] = projectile.localAI[1] + 7 - Main.rand.Next(framesPerSnowflake);
            }
        }
    }
}
