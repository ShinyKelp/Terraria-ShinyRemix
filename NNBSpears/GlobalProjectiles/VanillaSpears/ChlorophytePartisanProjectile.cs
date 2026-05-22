using Terraria.ID;
using Terraria;
using ShinyRemix.NNBSpears.GlobalProjectiles;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class ChlorophytePartisanProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.ChlorophytePartisan;
        protected override float HoldoutRangeMax => 186f;
        protected override float HoldoutRangeMin => 32f;
        protected override bool ShootsProjectile => true;
        protected override int ShotProjectileID => ProjectileID.SporeCloud;
        protected override float ShotProjectileSpeed => 6f;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 600);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}