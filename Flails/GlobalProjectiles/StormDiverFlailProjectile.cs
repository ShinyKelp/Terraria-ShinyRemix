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
    public class StormDiverFlailProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.FlailChanges && ShinyUtils.StormDivers && entity.type == FlailUtils.VaporizerFlailProjID;
        }
        public override bool PreAI(Projectile projectile)
        {
            //Vaporizer needs the timer to be full numbers for its spike shoot condition
            projectile.localAI[1] = (float)Math.Round(projectile.localAI[1]);
            return base.PreAI(projectile);
        }    
    }
}
