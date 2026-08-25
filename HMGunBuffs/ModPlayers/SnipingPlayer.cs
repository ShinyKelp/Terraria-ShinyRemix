using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.HMGunBuffs.ModPlayers
{
    public class SnipingPlayer : ModPlayer
    {
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if(ShinyOptions.HMGunBuffs && Player.HeldItem.type == ItemID.SniperRifle && (Player.itemAnimation != 0 || (Player.scope && Main.mouseRight && !Player.mouseInterface)))
            {
                Vector2 startPos = Player.MountedCenter;
                Vector2 endPos = Main.MouseWorld;
                Vector2 direction = endPos - startPos;
                direction.Normalize();
                startPos += direction * 60f;
                float distance = (endPos - startPos).Length();
                float minDistance = 1400f;

                if(distance < minDistance)
                {
                    endPos += direction * (minDistance - distance);
                }

                Utils.DrawLine(Main.spriteBatch,
                    startPos,
                    endPos,
                    new Color(255, 30, 30, 255), 
                    Color.Transparent, 2f);
            }
        }
    }
}
