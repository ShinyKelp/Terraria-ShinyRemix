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
    public class GladiatorSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 114f;
        protected override float HoldoutRangeMin => 12f;
        protected override float HoldPositionRelative => 0.8f;
        protected override string ModSpearName => "GladiatorSpearProj";

        protected override bool HasDustParticles => true;

        protected override void CreateDustParticles(Projectile projectile)
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 228, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 1, default(Color), 1.2f);
                dust.noGravity = true;
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}
