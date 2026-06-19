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
    public class StormBowArrows : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics;
        }
        public bool origCollide = true;
        public bool stormArrow = false;
        public float targetHeight = 0f;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            if (player.HeldItem.type == ItemID.DaedalusStormbow)
            {
                stormArrow = true;
                targetHeight = player.position.Y - 200f;
                origCollide = projectile.tileCollide;
                projectile.tileCollide = false;
            }
            base.OnSpawn(projectile, source);
        }

        public override void PostAI(Projectile projectile)
        {
            if(stormArrow)
            {
                if(projectile.position.Y > targetHeight)
                {
                    projectile.tileCollide = origCollide;
                    stormArrow = false;
                }
            }
            base.PostAI(projectile);
        }
    }
}
