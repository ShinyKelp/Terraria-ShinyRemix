using Microsoft.Xna.Framework;
using ShinyRemix.PostMechMimics.ModProjectiles;
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
    public class ClingerWall : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ProjectileID.ClingerStaff;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.ai[2] = 0;
            if (projectile.ai[1] < 0)
            {
                projectile.Kill();
            }
            base.OnSpawn(projectile, source);
        }
        const int framesPerPulse = 20;
        //Reconstructed from Vanilla code

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            return false;
        }
        public override bool PreAI(Projectile projectile)
        {
            projectile.position.Y = projectile.ai[0];
            projectile.height = (int)projectile.ai[1];

            if (projectile.Center.X > Main.player[projectile.owner].Center.X)
                projectile.direction = 1;
            else
                projectile.direction = -1;
            projectile.velocity.X = (float)projectile.direction * 1E-06f;
            if (projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active &&
                        i != projectile.whoAmI &&
                        Main.projectile[i].type == projectile.type &&
                        Main.projectile[i].owner == projectile.owner &&
                        Main.projectile[i].timeLeft > projectile.timeLeft)
                    {
                        projectile.Kill();
                        return false;
                    }
                }
            }

            if (projectile.ai[2] == 0)
            {
                projectile.ai[2] = framesPerPulse;
                if(projectile.owner == Main.myPlayer)
                {
                    Vector2 spawnPos = projectile.position;
                    spawnPos.Y += projectile.height;
                    spawnPos.X += 16;
                    int proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<ClingerStaffPulse>(),
                        projectile.damage, projectile.knockBack, projectile.owner, projectile.height);
                }
            }
            projectile.ai[2]--;
            return false;

            float totalDustParticles = (float)(projectile.width * projectile.height) * 0.0045f;
            int dustCreated = 0;
            while ((float)dustCreated < totalDustParticles)
            {
                int newDustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[newDustID];
                dust.noGravity = true;
                dust.scale = 1.4f;
                dust.velocity *= 0.5f;
                dust.velocity.Y -= 0.5f;
                dust.position.X += 6f;
                dust.position.Y -= 2f;
                dustCreated++;
            }
            return false;
        }
    }
}
