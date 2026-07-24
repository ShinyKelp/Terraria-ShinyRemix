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
using static Terraria.GameContent.Animations.IL_Actions.Sprites;

namespace ShinyRemix.PreMechMage.ModProjectiles
{
    public class CursedFlamesStream : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.aiStyle = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.scale *= 0.8f;
            Projectile.velocity *= 0.8f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity * 0.95f;
            Projectile.position -= Projectile.velocity;
            return false;
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            int num = 60;
            int num2 = 12;
            int num3 = num + num2;
            if (Projectile.localAI[0] >= (float)num3)
            {
                Projectile.Kill();
            }
            if (Projectile.localAI[0] >= (float)num)
            {
                Projectile.velocity *= 0.90f;
            }
            int num4 = 50;
            if (Projectile.localAI[0] < (float)num4 && Main.rand.NextFloat() < 0.25f)
            {
                int num6 = DustID.CursedTorch;
                Dust dust = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Projectile.localAI[0], 0f, 72f, 0.5f, 1f, true), 4, 4, num6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                if (Main.rand.Next(4) == 0)
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    Dust dust2 = dust;
                    dust2.velocity.X = dust2.velocity.X * 2f;
                    Dust dust3 = dust;
                    dust3.velocity.Y = dust3.velocity.Y * 2f;
                }
                else
                {
                    dust.scale *= 1.5f;
                }
                dust.scale *= 1.5f;
                dust.velocity *= 1.2f;
                dust.velocity += Projectile.velocity * 1f * Utils.Remap(Projectile.localAI[0], 0f, num * 0.75f, 1f, 0.1f, true) * Utils.Remap(Projectile.localAI[0], 0f, num * 0.1f, 0.1f, 1f, true);
                dust.customData = 1;
                dust.scale *= 0.6f;
            }
            if (num4 > 0 && Projectile.localAI[0] >= (float)num4 && Main.rand.NextFloat() < 0.5f)
            {
                Vector2 center = Main.player[Projectile.owner].Center;
                Vector2 vector = (Projectile.Center - center).SafeNormalize(Vector2.Zero).RotatedByRandom(0.19634954631328583) * 7f;
                short num7 = 31;
                Dust dust4 = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) - vector * 2f, 4, 4, (int)num7, 0f, 0f, 150, new Color(80, 80, 80), 1f);
                dust4.noGravity = true;
                dust4.velocity = vector;
                dust4.scale *= 1.1f + Main.rand.NextFloat() * 0.2f;
                dust4.customData = -0.3f - 0.15f * Main.rand.NextFloat();
            }
        }

        //Draw functionality adapted from TRAE's custom flamethrower effects (which is, itself, adapted from vanilla)

        Color ColorMiddle = new Color(50, 255, 0, 200);
        Color ColorBack = new Color(167, 255, 65, 200);
        Color ColorSmoke = new Color(70, 70, 70, 100);
        float minScale = 0.25f;
        float maxScale = 1f;
        public override bool PreDraw(ref Color lightColor)
        {
            DrawFlamethrower(ColorMiddle, ColorBack, Color.Lerp(ColorMiddle, ColorBack, 0.25f), ColorSmoke);
            return false;
        }
        public void DrawFlamethrower(Color color1, Color color2, Color color3, Color color4)
        {
            Main.instance.LoadProjectile(ProjectileID.Flames);
            float num = 60f;
            float num2 = 12f;
            float fromMax = num + num2;
            Texture2D value = TextureAssets.Projectile[ProjectileID.Flames].Value;
            Color baseFIreColor = Color.Transparent;
            float num3 = 0.35f;
            float num4 = 0.7f;
            float num5 = 0.85f;
            float incrementForAfterImages = Projectile.localAI[0] > num - 10f ? 0.175f : 0.2f;

            int num7 = 3;
            int num8 = 2;
            int num9 = 7;
            int num10 = num9 * num8 * num7;
            float num11 = Utils.Remap(Projectile.localAI[0], num, fromMax, 1f, 0f);
            float num12 = Math.Min(Projectile.localAI[0], 20f);
            float num13 = Utils.Remap(Projectile.localAI[0], 0f, fromMax, 0f, 1f);
            float scale = Utils.Remap(num13, 0.2f, 0.5f, minScale, maxScale);
            Rectangle rectangle = value.Frame(1, num9, 0, 3);
            if (!(num13 < 1f))
            {
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                for (float j = 1; j >= 0f; j -= incrementForAfterImages)
                {
                    baseFIreColor = num13 < 0.1f ? Color.Lerp(Color.Transparent, color1, Utils.GetLerpValue(0f, 0.1f, num13, clamped: true)) : num13 < 0.2f ? Color.Lerp(color1, color2, Utils.GetLerpValue(0.1f, 0.2f, num13, clamped: true)) : num13 < num3 ? color2 : num13 < num4 ? Color.Lerp(color2, color3, Utils.GetLerpValue(num3, num4, num13, clamped: true)) : num13 < num5 ? Color.Lerp(color3, color4, Utils.GetLerpValue(num4, num5, num13, clamped: true)) : !(num13 < 1f) ? Color.Transparent : Color.Lerp(color4, Color.Transparent, Utils.GetLerpValue(num5, 1f, num13, clamped: true));
                    float num16 = (1f - j) * Utils.Remap(num13, 0f, 0.2f, 0f, 1f);
                    Vector2 vector = Projectile.Center - Main.screenPosition + Projectile.velocity * (0f - num12) * j;
                    Color color5 = baseFIreColor * num16;
                    Color color6 = color5;
                    color6.G /= 2;
                    color6.B /= 2;
                    color6.A = (byte)Math.Min((float)(int)color5.A + 80f * num16, 255f);
                    float num17 = Utils.Remap(Projectile.localAI[0], 20f, fromMax, 0f, 1f);
                    num17 *= num17;
                    
                    float num18 = 1f / incrementForAfterImages * (j + 1f);
                    float num19 = Projectile.rotation + j * (MathF.PI / 2f) + Main.GlobalTimeWrappedHourly * num18 * 2f;
                    float num20 = Projectile.rotation - j * (MathF.PI / 2f) - Main.GlobalTimeWrappedHourly * num18 * 2f;
                    switch (i)
                    {
                        case 0:
                            Main.EntitySpriteDraw(value,
                                vector + Projectile.velocity * (0f - num12) * incrementForAfterImages * 0.5f,
                                rectangle,
                                color6 * num11 * 0.25f,
                                num19 + MathF.PI / 4f,
                                rectangle.Size() / 2f,
                                scale,
                                SpriteEffects.None);
                            Main.EntitySpriteDraw(value, vector, rectangle, color6 * num11, num20, rectangle.Size() / 2f, scale, SpriteEffects.None);
                            //Main.EntitySpriteDraw(value, vector, rectangle, new Color(255,255,255,0) * num11 * num16, num20, rectangle.Size() / 2f, scale * 0.35f, SpriteEffects.None);
                            break;
                        case 1:
                                Main.EntitySpriteDraw(value, vector + Projectile.velocity * (0f - num12) * incrementForAfterImages * 0.2f, rectangle, color5 * num11 * 0.25f, num19 + MathF.PI / 2f, rectangle.Size() / 2f, scale * 0.75f, SpriteEffects.None);
                                Main.EntitySpriteDraw(value, vector, rectangle, color5 * num11, num20 + MathF.PI / 2f, rectangle.Size() / 2f, scale * 0.75f, SpriteEffects.None);
                            
                            float whiteOpacity = 1 - Utils.GetLerpValue(num4, num5, num13 - .05f, clamped: true);
                            whiteOpacity *= whiteOpacity;
                            Main.EntitySpriteDraw(value, vector, rectangle, new Color(255, 255, 255, 0) * (num11 * num11) * num16 * 0.35f * whiteOpacity, -num20 + MathF.PI / 2f, rectangle.Size() / 2f, scale * 0.45f, SpriteEffects.None);
                            Main.EntitySpriteDraw(value, vector, rectangle, new Color(255, 255, 255, 0) * (num11 * num11) * num16 * 0.35f * whiteOpacity, num20 - MathF.PI / 2f, rectangle.Size() / 2f, scale * 0.4f, SpriteEffects.None);
                            break;
                    }
                }
            }
            return;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int num = (int)(Utils.Remap(Projectile.localAI[0], 0f, 72f, 10f, 40f) * maxScale);
            hitbox.Inflate(num, num);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.localAI[0] > 65)
                return false;
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 600);
        }
    }
}
