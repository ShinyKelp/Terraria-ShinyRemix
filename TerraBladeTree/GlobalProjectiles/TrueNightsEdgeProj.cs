using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.TerraBladeTree.GlobalProjectiles
{
    public class TrueNightsEdgeProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.TrueNightsEdge;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            refScale = projectile.scale;
            totalTime = projectile.timeLeft;
            base.OnSpawn(projectile, source);
        }
        private int totalTime = 0;
        private float refScale = 1f;
        public override void AI(Projectile projectile)
        {
            base.AI(projectile);
            projectile.velocity.Normalize();
            projectile.velocity *= 1.5f;
            refScale *= 1.0064f;
            projectile.scale = refScale;
        }

    }
}
