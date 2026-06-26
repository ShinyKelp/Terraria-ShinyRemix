using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalProjectiles
{
    internal class TinyEater : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ProjectileID.TinyEater;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.timeLeft = 110;
            if(source is EntitySource_Parent parent)
            {
                if(parent.Entity is Projectile proj && proj.owner == Main.myPlayer)
                {
                    if(proj.TryGetGlobalProjectile<CorruptorScourgeProj>(out CorruptorScourgeProj corruptorProj))
                    {
                        int baseValue = 7;
                        if (ShinyUtils.TRAE && ShinyOptions.SpearRework)
                            baseValue = 11;
                        if (Main.rand.Next(1, baseValue - corruptorProj.hitEnemies) != 1)
                            projectile.Kill();
                        
                    }
                }
            }
        }
    }
}
