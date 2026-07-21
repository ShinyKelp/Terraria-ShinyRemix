using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.Common.ModDusts
{
    public class HealDust : ModDust
    {
        public override string Texture => "Terraria/Images/Dust";


        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
            dust.noLightEmittence = false;
            dust.noGravity = true;
            dust.scale *= 1.2f;

            if(Main.rand.NextFloat() < 0.6f)
            {
                dust.frame = new Rectangle(
                    50,
                    30,
                    8,
                    8
                    );
            }
            else
            {
                int frameY = Main.rand.NextBool() ? 40 : 50;
                dust.frame = new Rectangle(
                    50,
                    frameY,
                    8,
                    8
                );
            }
            dust.alpha = 40;
            dust.velocity = new Vector2(Main.rand.NextFloat(), Main.rand.NextFloat()) * 24f - new Vector2(12f, 12f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity = Vector2.Lerp(dust.velocity, new Vector2(0f, -2f), 0.35f);
            dust.scale -= 0.02f;

            if (dust.scale < 0.2f)
                dust.active = false;

            Lighting.AddLight(
                (int)(dust.position.X / 16f), 
                (int)(dust.position.Y / 16f), 
                1f, 
                0.2f, 
                0.25f);

            return false;
        }
    }
}
