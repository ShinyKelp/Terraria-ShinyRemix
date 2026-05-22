using ShinyRemix.NNBSpears.GlobalProjectiles;
using Terraria;
using Terraria.ID;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class MushroomSpearProjectile : SpearProjectileBase
    {
        protected override int SpearID => ProjectileID.MushroomSpear;
        protected override float HoldoutRangeMax => 160f;
        protected override float HoldoutRangeMin => 24f;
        protected override bool ShootsProjectile => true;
        protected override int ShotProjectileID => ProjectileID.Mushroom;
        protected override float ShotProjectileSpeed => 12f;
        private float[] thresholds = { 0, .08f, .16f, .24f, .46f, .68f, .90f};
        private int mushroomsSpawned = 0;
        protected override void ShootProjectiles(Projectile projectile)
        {
            if(mushroomsSpawned < thresholds.Length)
            {
                Player player = Main.player[projectile.owner];
                int duration = player.itemAnimationMax;

                if(duration - projectile.timeLeft > duration * thresholds[mushroomsSpawned])
                {
                    float realShotProjectileSpeed = mushroomsSpawned >= 4 ? 0f : ShotProjectileSpeed;
                    Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity * realShotProjectileSpeed, ShotProjectileID, (int)(projectile.damage * 0.75f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
                    mushroomsSpawned++;
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 300);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}