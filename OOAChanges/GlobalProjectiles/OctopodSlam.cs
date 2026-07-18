using Microsoft.Xna.Framework;
using ShinyRemix.OOAChanges.GlobalItems;
using ShinyRemix.OOAChanges.ModPlayers;
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
    public class OctopodSlam : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ProjectileID.MonkStaffT1Explosion;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.timeLeft = 3600;
            projectile.penetrate = -1;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 20;
            projectile.localNPCHitCooldown = 20;
            Player player = Main.player[projectile.owner];
            player.GetModPlayer<OctopodReuseDelay>().nextReuseNerf = false;
            projectile.position.X += 48f * player.direction * (player.HeldItem.scale - 1f);
        }
        public override bool PreAI(Projectile projectile)
        {
            if(projectile.owner == Main.myPlayer)
            {
                if (projectile.ai[0] == 20f && projectile.ai[1] < 16f)
                {
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.damage = (int)Math.Floor(projectile.damage / 3f);
                        projectile.usesIDStaticNPCImmunity = true;
                        projectile.usesLocalNPCImmunity = false;
                    }
                    projectile.ai[1]++;
                    projectile.ai[0] = 5f;
                }
                CreateDust(projectile);

            }
            return base.PreAI(projectile);
        }

        private void CreateDust(Projectile projectile)
        {
            float dustAmount = (float)Math.Floor(Main.rand.NextFloat() * 7f) - 4f;
            if (dustAmount <= 0f) return;

            Vector2 spinningpoint = new Vector2(7f, 0f);
            Vector2 vector = new Vector2(1f, 0.7f);
            Color color = new Color(20, 255, 100, 200);
            Vector2 explosionPos = projectile.Bottom;
            explosionPos.Y -= 8f;
            for (float num = 0f; num < dustAmount; num++)
            {
                Vector2 vector2 = spinningpoint.RotatedBy((double)(num * 6.2831855f / 25f), default(Vector2)) * vector;
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 0, default(Color), 1f);
                dust.alpha = 50;                
                dust.color = color;
                dust.position = explosionPos + vector2;
                dust.velocity.Y = dust.velocity.Y - 3f;
                dust.velocity.X = dust.velocity.X * 0.5f;
                dust.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
                dust.noLight = true;
            }
        }
    }
}
