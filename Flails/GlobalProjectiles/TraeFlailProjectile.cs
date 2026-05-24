using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinyRemix.Flails.ModProjectiles;
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

namespace ShinyRemix.Flails.GlobalProjectiles
{
    public class TraeFlailProjectile : BaseFlailProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyUtils.TRAE && entity.type == FlailUtils.FullMoonID;
        }
    }
}
