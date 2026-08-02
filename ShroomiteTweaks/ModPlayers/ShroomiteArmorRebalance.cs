using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.ShroomiteTweaks.ModPlayers
{
    public class ShroomiteArmorRebalance : ModPlayer
    {
        public override void PostUpdate()
        {
            if (ShinyOptions.ShroomiteTweaks && Player.shroomiteStealth && Player.stealth < 1f)
            {
                Player.GetDamage(DamageClass.Ranged) -= (1f - Player.stealth) * 0.6f;
                Player.GetCritChance(DamageClass.Ranged) -= (float)((int)((1f - Player.stealth) * 10f));

                if(Player.stealth > 0.4f)
                {
                    if ((Math.Abs(Player.velocity.X) > 0.1f || Math.Abs(Player.velocity.Y) > 0.1f))
                    {
                        if (Player.stealth > 0.4f)
                        {
                            Player.stealth -= (Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y)) * 0.0075f;
                            Player.stealth -= 0.002f;
                            Player.stealth = Math.Max(Player.stealth, 0.4f);
                        }
                    }
                    else if (Player.stealthTimer != 0f)
                    {
                        Player.stealth -= 0.002f;
                        Player.stealth = Math.Max(Player.stealth, 0.4f);
                    }

                }


                Player.GetDamage(DamageClass.Ranged) += (1f - Player.stealth) * 0.6f;
                Player.GetCritChance(DamageClass.Ranged) += (float)((int)((1f - Player.stealth) * 10f));
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (ShinyOptions.ShroomiteTweaks && Player.shroomiteStealth)
                Player.stealth = 1f;
        }
    }
}
