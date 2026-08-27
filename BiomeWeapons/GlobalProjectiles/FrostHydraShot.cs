using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalProjectiles
{
    public class FrostHydraShot : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ProjectileID.FrostBlastFriendly;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (!projectile.usesLocalNPCImmunity)
            {
                projectile.usesLocalNPCImmunity = true;
                projectile.usesIDStaticNPCImmunity = false;
                projectile.localNPCHitCooldown = 10;
            }
        }
    }
}
