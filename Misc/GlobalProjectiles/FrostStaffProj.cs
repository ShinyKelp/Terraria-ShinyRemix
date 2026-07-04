using Microsoft.Xna.Framework;
using ShinyRemix.Misc.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Misc.GlobalProjectiles
{
    public class FrostStaffProj : GlobalProjectile
    {
        private int hitNPC = -1;
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.Misc && entity.type == ProjectileID.FrostBoltStaff;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = 1;
            projectile.velocity *= 0.7f;
            base.OnSpawn(projectile, source);
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitNPC = target.whoAmI;
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if(projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(), 
                    projectile.Center, 
                    Vector2.Zero, 
                    ModContent.ProjectileType<FrostBlastMagic>(), 
                    (int)Math.Round(projectile.damage*0.67f), 
                    projectile.knockBack * 0.5f, 
                    projectile.owner,
                    Math.Sign(projectile.velocity.X),
                    40,
                    hitNPC);
            }
            base.OnKill(projectile, timeLeft);
        }
    }
}
