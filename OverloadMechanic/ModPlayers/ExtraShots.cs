using ShinyRemix.OverloadMechanic.GlobalProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.OverloadMechanic.ModPlayers
{
    public class ExtraShots : ModPlayer
    {
        List<OverloadShot> extraShots = new List<OverloadShot>();
        public void AddExtraShot(OverloadShot shot)
        {
            extraShots.Add(shot);
        }
        public override void PreUpdate()
        {
            if (Main.myPlayer != Player.whoAmI)
                return;
            for (int i = extraShots.Count - 1; i >= 0; i--)
            {
                extraShots[i].counter--;
                if(extraShots[i].counter < 0)
                {
                    if (Player.HasItem(extraShots[i].AmmoUsed) && CheckDistance(extraShots[i].originalProjectile))
                    {
                        OverloadShot shot = extraShots[i];
                        Player.ConsumeItem(shot.AmmoUsed);
                        Projectile proj = Projectile.NewProjectileDirect(shot.source, Player.Center - shot.PositionOffset, shot.Velocity, shot.Type, shot.Damage, shot.Knockback, Player.whoAmI, shot.ai0, shot.ai1, shot.ai2);
                        if (proj.TryGetGlobalProjectile<OverloadedProjectile>(out OverloadedProjectile overload))
                        {
                            overload.IsDuplicate = true;
                        }
                        if (proj.localAI[0] == 0f && shot.local0 != 0f)
                            proj.localAI[0] = shot.local0;
                        if (proj.localAI[1] == 0f && shot.local1 != 0f)
                            proj.localAI[1] = shot.local1;
                        if (proj.localAI[2] == 0f && shot.local2 != 0f)
                            proj.localAI[2] = shot.local2;
                    }
                    extraShots.RemoveAt(i);
                }
            }
        }

        bool CheckDistance(int projectileID)
        {
            if (!Main.projectile[projectileID].active)
                return true;
            return Player.Center.Distance(Main.projectile[projectileID].Center) > 32f;
        }
    }
}
