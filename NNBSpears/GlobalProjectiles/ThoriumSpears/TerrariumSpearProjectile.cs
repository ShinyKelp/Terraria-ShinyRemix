using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.ThoriumSpears
{
    public class TerrariumSpearProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 204f;
        protected override float HoldoutRangeMin => 36f;
        protected override string ModSpearName => "TerrariumSpearPro";
        protected override bool HasDustParticles => true;

        public static readonly int[] dusts = new int[]
        {
            90,
            174,
            87,
            89,
            92,
            88,
            86
        };
        protected override void CreateDustParticles(Projectile projectile)
        {
            if (Main.rand.NextBool(3))
            {
                for (int i = 0; i < dusts.Length; i++)
                {
                    int dust = dusts[i];
                    int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, dust, projectile .velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
                    Main.dust[DustID].noGravity = true;
                }
            }
        }
    }
}
