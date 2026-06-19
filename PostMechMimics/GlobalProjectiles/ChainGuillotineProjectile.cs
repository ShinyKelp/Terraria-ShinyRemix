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

namespace ShinyRemix.PostMechMimics.GlobalProjectiles
{
    public class ChainGuillotineProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private bool timeToRetreat = false;
        private Vector2 origVelocity = Vector2.Zero;
        private int hitResetTimer = 5;
        private int hitEnemies = 0;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ProjectileID.ChainGuillotine;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            origVelocity = projectile.velocity;
        }

        public override void PostAI(Projectile projectile)
        {
            if (!timeToRetreat)
            {
                Player player = Main.player[projectile.owner];
                if (Vector2.Distance(player.MountedCenter, projectile.Center) > 525f)
                    timeToRetreat = true;
            }
            if (timeToRetreat)
            {
                projectile.ai[0] = 1;
                if(hitResetTimer > 0)
                    hitResetTimer--;
                else if(hitResetTimer == 0)
                {
                    hitResetTimer = -1;
                    ResetAllHitCooldowns(projectile);
                }
            }
            else
            {
                projectile.ai[0] = 0;
                projectile.velocity = origVelocity;
            }
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 14, 0f, 0f, 180, default(Color), 1.3f);
                dust.velocity *= 0.3f;
                return;
            }
            base.PostAI(projectile);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitEnemies++;
            if (hitEnemies > 3 && !timeToRetreat)
                timeToRetreat = true;
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            timeToRetreat = true;
            return base.OnTileCollide(projectile, oldVelocity);
        }


        private void ResetAllHitCooldowns(Projectile projectile)
        {
            projectile.damage = (int)Math.Floor(projectile.damage * 0.66f);
            for(int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    projectile.localNPCImmunity[i] = 0;
                }
            }
        }
    }
}
