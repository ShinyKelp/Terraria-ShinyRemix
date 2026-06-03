using Microsoft.Xna.Framework;
using ShinyRemix.FrostWeapons.ModProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.FrostWeapons.GlobalProjectiles
{
    public class IceBladeShot : FrostBrandShot
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.FrostWeaponChanges && entity.type == ProjectileID.IceBolt;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.damage = (int)Math.Floor((float)projectile.damage * 2f / 3f);
            base.OnSpawn(projectile, source);
        }

        protected override Vector2 BaseShotDistance => new Vector2(-32f, -32f);

    }
}
