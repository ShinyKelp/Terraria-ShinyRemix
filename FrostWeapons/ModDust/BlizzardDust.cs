using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.FrostWeapons.ModDusts
{
    public class BlizzardDust : ModDust
    {
        public override string Texture => "Terraria/Images/Dust";


        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
            dust.noLightEmittence = false;
            dust.noGravity = true;
            dust.scale *= 1.2f;

            if(Main.rand.NextFloat() < 0.4f)
            {
                dust.frame = new Rectangle(
                    160,
                    0,
                    8,
                    8
                    );
            }
            else
            {
                int frameY = Main.rand.Next(3) * 10 + 30;
                dust.frame = new Rectangle(
                    800,
                    frameY,
                    8,
                    8
                );
            }
            dust.alpha = 40;
        }

        public override bool Update(Dust dust)
        {
            if (dust.customData is not BlizzardDustData blizzardData)
                return false;
            if(!dust.active)
                return false;

            Vector2 relativeCenter = blizzardData.relativeCenter;
            float radius = blizzardData.radius;

            Vector2 forwardVel = dust.position - relativeCenter;
            Vector2 forwardDir = forwardVel.SafeNormalize(Vector2.UnitX);

            dust.velocity = forwardDir * 6f;

            // Should fully spin at radius length.
            float distance = forwardVel.Length();
            float relativeDistance = distance / radius;

            dust.velocity *= MathHelper.Lerp(1f-relativeDistance, 1f, 0.3f);

            float swirlStrength = Math.Sign(blizzardData.rotDir) * MathHelper.PiOver2 * MathHelper.Lerp(relativeDistance, 0f, 0.3f);

            dust.velocity = dust.velocity.RotatedBy(swirlStrength);

            dust.position += dust.velocity;

            float trueScalingFactor = MathHelper.Lerp(1f, blizzardData.scaleDecreaseSpeed, relativeDistance);
            dust.scale *= trueScalingFactor;

            if (dust.scale < 0.2f)
                dust.active = false;

            Lighting.AddLight(
                (int)(dust.position.X / 16f), 
                (int)(dust.position.Y / 16f), 
                0f, 
                dust.scale * 0.8f, 
                dust.scale);

            return false;
        }

        public class BlizzardDustData
        {
            public Vector2 relativeCenter = Vector2.Zero;
            public float radius = 120f;
            public float scaleDecreaseSpeed = 0.975f;
            public float rotDir = 1f;
        }
    }
}
