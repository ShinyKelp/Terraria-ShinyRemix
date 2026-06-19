using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

namespace ShinyRemix.PostMechMimics.ModProjectiles
{
    public class ClingerStaffPulse : ModProjectile
    {
        public override string Texture => "Terraria/Images/Extra_0";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        const int totalLifespan = 100;
        bool spawnOnKill = false;
        int extraSpawnFrame = -1;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.Resize(24, 16);
            Projectile.velocity = new Vector2(0, -Projectile.ai[0] / totalLifespan);
            Projectile.timeLeft = totalLifespan;
            if (Main.rand.NextFloat() > 0.8f)
                spawnOnKill = true;
            if (Main.rand.NextFloat() > 0.35f)
                extraSpawnFrame = (int)Math.Floor(Main.rand.NextFloat() * 0.7f * totalLifespan);
        }
        public override void OnKill(int timeLeft)
        {
            if(spawnOnKill && Projectile.owner == Main.myPlayer)
            {
                SpawnFireball();
            }
            base.OnKill(timeLeft);
        }
        public override void AI()
        {
            if (Projectile.Center.X > Main.player[Projectile.owner].Center.X)
                Projectile.direction = 1;
            else
                Projectile.direction = -1;
            if(Projectile.timeLeft == extraSpawnFrame && Projectile.owner == Main.myPlayer)
            {
                SpawnFireball();
            }
            float totalDustParticles = (float)(Projectile.width * Projectile.height) * 0.0045f;
            float dustCreated = 0;
            Vector2 position = Projectile.position;
            position.X -= 8;
            while (dustCreated < totalDustParticles)
            {
                int newDustID = Dust.NewDust(position, Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[newDustID];
                dust.noGravity = true;
                dust.scale = 1.4f;
                dust.velocity *= 0.5f;
                dust.velocity.Y -= 0.5f;
                dust.position.X += 6f;
                dust.position.Y -= 2f;
                dustCreated++;
            }
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
        public override void PostDraw(Color lightColor)
        {
            //DrawHitbox();
            base.PostDraw(lightColor);
        }

        protected void SpawnFireball()
        {
            Vector2 velocity = Vector2.Zero;
            float baseVel = 8f;
            velocity.Y = -1 * (Main.rand.NextFloat() * baseVel / 2f + baseVel);
            velocity.X = ((Main.rand.NextFloat() - 0.5f) * baseVel / 2f);
            Vector2 position = Projectile.position;
            position.X += 12;
            position.Y += 8;
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<ClingerStaffFireSpit>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }

        protected void DrawHitbox()
        {
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            Rectangle hitbox = Projectile.Hitbox;
            hitbox.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);

            Main.spriteBatch.Draw(pixel, hitbox, Color.Red * 0.5f);
        }
    }
}
