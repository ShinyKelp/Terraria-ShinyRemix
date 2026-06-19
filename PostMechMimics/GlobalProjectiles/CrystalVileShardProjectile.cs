using Microsoft.Xna.Framework;
using ShinyRemix.PostMechMimics.GlobalItems;
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
    public class CrystalVileShardProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        bool resetIFrames = false;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && (entity.type == ProjectileID.CrystalVileShardHead ||
                entity.type == ProjectileID.CrystalVileShardShaft);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            //Making the projectile longer
            if (projectile.ai[1] == 0)
            {
                
                if (Main.player[projectile.owner].HeldItem.TryGetGlobalItem<VileShard>(out VileShard weaponShard))
                {
                    projectile.localAI[1] = weaponShard.shardID;    //LocalAI[0] is apparently used for the sound?
                }
                projectile.rotation = projectile.velocity.ToRotation() + (Main.rand.NextFloat() - 0.5f) * MathHelper.PiOver4 * 0.2f;
                projectile.velocity = projectile.rotation.ToRotationVector2() * projectile.velocity.Length();
            }
            else
            {
                
                if (source is EntitySource_Parent parentSource)
                {
                    if (parentSource.Entity is Projectile parent)
                    {
                        projectile.localAI[1] = parent.localAI[1];
                    }
                }
            }

            if (projectile.ai[1] == 2)
            {
                projectile.ai[1] = (float)Math.Floor(-Main.rand.NextFloat() * 4f - 3f);
            }
            else if (projectile.ai[1] == -1)
                projectile.ai[1] = 3;
            else if (projectile.ai[1] == 7)
                projectile.ai[1] = 8;
            projectile.extraUpdates = 1;    //Convenient way to make the projectile's animation faster
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            base.OnSpawn(projectile, source);
        }

        public override bool PreAI(Projectile projectile)
        {
            if(!resetIFrames && projectile.alpha > 160 && projectile.alpha < 250)
            {
                resetIFrames = true;
                ResetAllHitCooldowns(projectile);
            }
            return base.PreAI(projectile);
        }

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            for(int i = 0; i < Main.maxProjectiles; ++i)
            {
                if (Main.projectile[i].type == ProjectileID.CrystalVileShardHead 
                    || Main.projectile[i].type == ProjectileID.CrystalVileShardShaft)
                {
                    if (Main.projectile[i].localAI[1] == projectile.localAI[1])
                    {
                        if (Main.projectile[i].localNPCImmunity[target.whoAmI] != 0)
                            return false;
                    }
                }
            }
            return base.CanHitNPC(projectile, target);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        private void ResetAllHitCooldowns(Projectile projectile)
        {
            projectile.damage = (int)Math.Floor(projectile.damage * 1.5f);
            projectile.knockBack *= 1.5f;
            projectile.ArmorPenetration += 10;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    projectile.localNPCImmunity[i] = 0;
                }
            }
        }
    }
}
