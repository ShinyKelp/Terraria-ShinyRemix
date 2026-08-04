using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PreMechMage.ModProjectiles
{
    public class BloodThornProj : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PreMechMage && entity.type == ProjectileID.SharpTears;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Bleeding, 600);
        }
    }
}
