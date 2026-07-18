using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.OOAChanges.GlobalProjectiles
{
    public class SkydragonSphere : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.Electrosphere;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.ai[2] != 0f)
            {
                projectile.timeLeft = 10;
                projectile.usesIDStaticNPCImmunity = true;
                projectile.usesLocalNPCImmunity = false;
                projectile.idStaticNPCHitCooldown = 10;
            }
            base.OnSpawn(projectile, source);
        }

        public override void PostAI(Projectile projectile)
        {
            if (projectile.ai[2] != 0f && projectile.timeLeft == 1)
                projectile.Kill();
        }
    }
}
