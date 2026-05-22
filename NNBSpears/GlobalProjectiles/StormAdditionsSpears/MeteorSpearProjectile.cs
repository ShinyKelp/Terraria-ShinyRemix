using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class MeteorSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 108f;
        protected override float HoldoutRangeMin => 16f;
        protected override float HoldPositionRelative => 0.76f;
        protected override float HitboxSizeScale => 1.1f;
        protected override string ModSpearName => "MeteorSpearProj";
   
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            projectile.scale *= 1.2f;
        }

    }
}
