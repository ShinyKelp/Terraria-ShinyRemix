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
    public class FlyingKnifeProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        private bool spawnedDuplicate = false;
        public Queue<KnifeData> oldPositions = new Queue<KnifeData>();
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ProjectileID.FlyingKnife;
        }
        public override void SetDefaults(Projectile entity)
        {
            entity.usesIDStaticNPCImmunity = true;
            entity.idStaticNPCHitCooldown = 10;
        }
        public override void PostAI(Projectile projectile)
        {
            oldPositions.Enqueue(new KnifeData { position = projectile.position, rotation = projectile.rotation, direction = projectile.direction});
            if(!spawnedDuplicate && oldPositions.Count >= 20)
            {
                spawnedDuplicate = true;
                KnifeData firstData = oldPositions.Dequeue();
                if (projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), firstData.position, Vector2.Zero, ModContent.ProjectileType<FlyingKnifeDuplicate>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                }
            }
            base.PostAI(projectile);
        }
    }
    public struct KnifeData
    {
        public Vector2 position;
        public float rotation;
        public int direction;
    }
}
