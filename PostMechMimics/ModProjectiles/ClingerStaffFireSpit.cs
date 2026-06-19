using Microsoft.Xna.Framework;
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
    public class ClingerStaffFireSpit : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_95";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 200;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.light = 0.8f;
            Projectile.alpha = 100;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.timeLeft = 70 + (int)Math.Floor(Main.rand.NextFloat() * 40f);
        }


        public override void AI()
        {
            if(Projectile.velocity.Y < 30f)
                Projectile.velocity.Y += 0.2f;
            Projectile.rotation += MathHelper.PiOver4 * Math.Sign(Projectile.velocity.X);
            int dustID = Dust.NewDust(new Vector2(Projectile.position.X + Projectile.velocity.X, Projectile.position.Y + Projectile.velocity.Y), Projectile.width, Projectile.height, 75, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 3f * Projectile.scale);
            Main.dust[dustID].noGravity = true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.Center.X > Main.player[Projectile.owner].Center.X)
                modifiers.HitDirectionOverride = 1;
            else
                modifiers.HitDirectionOverride = -1;

            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 600);
            base.OnHitNPC(target, hit, damageDone);
        }

    }

}
