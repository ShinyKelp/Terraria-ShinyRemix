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

namespace ShinyRemix.OOAChanges.GlobalProjectiles
{
    public class WisdomBookProjectile : GlobalProjectile
    {

        const float MaxBounces = 4;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.BookStaffShot;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.damage = (int)Math.Floor(projectile.damage * 1.1f);
            projectile.knockBack *= 1.2f;
            base.OnSpawn(projectile, source);
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if(projectile.owner == Main.myPlayer)
            {
                projectile.localAI[0]++;
                if (projectile.localAI[0] > MaxBounces)
                    return true;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = oldVelocity.X * -1f;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = oldVelocity.Y * -1f;
                }
                int num29 = projectile.FindTargetWithLineOfSight(800f);
                if (num29 != -1)
                {
                    NPC nPC2 = Main.npc[num29];
                    projectile.Distance(nPC2.Center);
                    projectile.velocity = projectile.DirectionTo(nPC2.Center).SafeNormalize(-Vector2.UnitY) * projectile.velocity.Length();
                    projectile.netUpdate = true;
                }
                return false;
            }
            return true;
        }
    }
}
