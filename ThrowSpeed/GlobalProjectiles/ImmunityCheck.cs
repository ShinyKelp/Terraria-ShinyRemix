using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShinyRemix.ThrowSpeed.GlobalProjectiles
{
    public class ImmunityCheck : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.ThrowSpeedScaling && entity.DamageType == DamageClass.Melee;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_ItemUse itemSource && ThrowSpeedUtils.ThrowItems.Contains(itemSource.Item.type))
            {
                if (!projectile.usesLocalNPCImmunity)
                {
                    if (projectile.usesIDStaticNPCImmunity)
                        projectile.localNPCHitCooldown = projectile.idStaticNPCHitCooldown;
                    else
                        projectile.localNPCHitCooldown = 10;
                    
                    projectile.usesIDStaticNPCImmunity = false;
                    projectile.usesLocalNPCImmunity = true;
                }
            }
        }
    }
}
