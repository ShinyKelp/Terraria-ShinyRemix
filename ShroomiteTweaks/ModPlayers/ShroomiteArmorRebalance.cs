using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.ShroomiteTweaks.ModPlayers
{
    public class ShroomiteArmorRebalance : ModPlayer
    {
        float thisFrameMovementValue = 0f;
        float minStealthThisFrame = 0f;
        int hurtCountdown = 0;
        public override void PreUpdate()
        {
            if ((Math.Abs(Player.velocity.X) > 0.1f || Math.Abs(Player.velocity.Y) > 0.1f))
                thisFrameMovementValue = (Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y)) * 0.0075f;
            else thisFrameMovementValue = 0f;
            minStealthThisFrame = Player.stealth;
        }
        public override void PostUpdate()
        {
            if (ShinyOptions.ShroomiteTweaks && Player.shroomiteStealth)
            {
                if(hurtCountdown > 0)
                {
                    hurtCountdown--;
                    Player.stealth = 1f;
                }
                else if (Player.stealth > 0.4f)
                {
                    Player.GetDamage(DamageClass.Ranged) -= (1f - Player.stealth) * 0.6f;
                    Player.GetCritChance(DamageClass.Ranged) -= (float)((int)((1f - Player.stealth) * 10f));
                    Player.GetKnockback(DamageClass.Ranged) /= 1f + (1f - Player.stealth) * 0.5f;

                    if (thisFrameMovementValue != 0f)
                    {
                        //Compensate for movement loss
                        Player.stealth = Math.Max(minStealthThisFrame, Player.stealth - thisFrameMovementValue);

                        //Add extra if not firing weapon
                        if (Player.itemAnimation == 0)
                            Player.stealth -= 0.0028f;

                        //Cap at 0.4 stealth
                        Player.stealth = Math.Max(Player.stealth, 0.4f);
                        if (Player.stealth > 0.39f && Player.stealth < 0.41f)
                            Player.stealth = 0.4f;
                    }

                    Player.GetDamage(DamageClass.Ranged) += (1f - Player.stealth) * 0.6f;
                    Player.GetCritChance(DamageClass.Ranged) += (float)((int)((1f - Player.stealth) * 10f));
                    Player.GetKnockback(DamageClass.Ranged) *= 1f + (1f - Player.stealth) * 0.5f;

                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (ShinyOptions.ShroomiteTweaks && Player.shroomiteStealth)
                hurtCountdown = 10;
        }
    }
}
