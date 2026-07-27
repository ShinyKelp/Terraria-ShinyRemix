using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PirateInvasionBuffs.GlobalProjectiles
{
    public class CutlassSlash : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.Muramasa;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
        }
        //To-Do: Make it gray/white.
    }
}
