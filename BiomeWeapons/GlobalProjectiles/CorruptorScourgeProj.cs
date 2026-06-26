using Microsoft.Xna.Framework;
using ShinyRemix.BiomeWeapons.ModBuffs;
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
    public class CorruptorScourgeProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ProjectileID.EatersBite;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.penetrate = 4;
            projectile.usesLocalNPCImmunity = true;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.localNPCHitCooldown = -1;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitEnemies++;
            //Dust code adapted from vanilla
            for (int i = 0; i < 15; i++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, 0f, 0f, 0, default(Color), 1f);
                Dust dust = Main.dust[dustID];
                dust.noGravity = true;
            }

            //Tiny eater spawn code adapted from vanilla
            if (projectile.owner == Main.myPlayer)
            {
                int amountOfTinies = 1;
                if (Main.rand.Next(1, 4) == 1)
                {
                    amountOfTinies = 2;
                }
                for (int i = 0; i < amountOfTinies; i++)
                {
                    float velX = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float velY = (float)Main.rand.Next(-35, 36) * 0.02f;
                    velX *= 10f;
                    velY *= 10f;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.position.X, projectile.position.Y, velX, velY, 307, (int)((double)projectile.damage * 0.75), (float)((int)((double)projectile.knockBack * 0.35)), Main.myPlayer, 0f, 0f, 0f);
                }
            }
        }
        public bool hitTile = false;
        public int hitEnemies = 0;
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            hitTile = true;
            return base.OnTileCollide(projectile, oldVelocity);
        }
    }
}
