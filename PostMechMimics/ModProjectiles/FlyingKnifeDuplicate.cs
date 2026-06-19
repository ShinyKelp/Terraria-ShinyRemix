using Microsoft.Xna.Framework;
using ShinyRemix.PostMechMimics.GlobalProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.ModProjectiles
{
    public class FlyingKnifeDuplicate : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 200;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 3000;
            Projectile.light = 0.8f;
            Projectile.alpha = 100;
            Projectile.ArmorPenetration = 10;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile parent = Main.projectile[(int)Projectile.ai[0]];
            if (parent != null && parent.TryGetGlobalProjectile<FlyingKnifeProjectile>(out var flyingKnifeDuplicate))
            {
                Projectile.owner = parent.owner;
                Projectile.timeLeft = parent.timeLeft;
                parentKnife = flyingKnifeDuplicate;
            }
            else
                Projectile.Kill();
        }
        private FlyingKnifeProjectile parentKnife;
        public override void AI()
        {
            if(parentKnife.oldPositions.Count == 0)
            {
                Projectile.Kill();
                return;
            }
            KnifeData data = parentKnife.oldPositions.Dequeue();
            Projectile.position = data.position;
            Projectile.rotation = data.rotation;
            Projectile.direction = data.direction;
            base.AI();
        }

        public override void PostAI()
        {
            int dustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.LightPink, 1.2f);
            Dust dust = Main.dust[dustID];
            dust.position = (Main.dust[dustID].position + Projectile.Center) / 2f;
            dust.noGravity = true;
            dust.velocity *= 0.3f;
            dust.velocity -= Projectile.velocity * 0.1f;
        }
    }
}
