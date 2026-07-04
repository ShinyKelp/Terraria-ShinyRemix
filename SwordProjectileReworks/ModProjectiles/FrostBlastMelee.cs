using Humanizer;
using Microsoft.Xna.Framework;
using ShinyRemix.Common.ModDusts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Cinematics;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.SwordProjectileReworks.ModProjectiles
{
    public class FrostBlastMelee : ModProjectile
    {
        private int immuneNPC = -1;
        private int frameCount = 0;
        public override string Texture => "Terraria/Images/Extra_0";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 110;
            Projectile.height = 110;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.timeLeft = 26;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.ai[1] > 0f)
                Projectile.timeLeft = (int)Math.Floor(Projectile.ai[1]);
            immuneNPC = (int)Projectile.ai[2];
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (frameCount < 10 && target.whoAmI == immuneNPC)
                return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Projectile.damage < 30)
                target.AddBuff(BuffID.Frostburn, 420);
            else
                target.AddBuff(BuffID.Frostburn2, 600);

            base.OnHitNPC(target, hit, damageDone);
        }

        const int dustDensity = 6;
        public override void AI()
        {
            frameCount++;
            for (int i = 0; i < dustDensity; i++)
            {
                Vector2 spawnPos = Projectile.Center;
                spawnPos.X -= 12;
                spawnPos.Y -= 12;
                int dustID =  Dust.NewDust(
                    spawnPos,
                    24,
                    24,
                    ModContent.DustType<BlizzardDust>(),
                    0f, 0f,
                    140,
                    default(Color),
                    1.6f);

                Dust dust = Main.dust[dustID];

                BlizzardDust.BlizzardDustData data = new BlizzardDust.BlizzardDustData()
                {
                    radius = 70f,
                    relativeCenter = Projectile.Center,
                    scaleDecreaseSpeed = 0.845f + Main.rand.NextFloat() * 0.06f,
                    rotDir = Projectile.ai[0]
                };
                Vector2 newLocation = (dust.position + Projectile.Center*2f) / 3f;
                dust.position = newLocation;
                dust.customData = data;

            }
        }
    }
}
