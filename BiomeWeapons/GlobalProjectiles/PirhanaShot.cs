using ShinyRemix.BiomeWeapons.GlobalNPCs;
using ShinyRemix.BiomeWeapons.ModBuffs;
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
    public class PirhanaShot : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons && entity.type == ProjectileID.MechanicalPiranha;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.velocity *= 1.2f;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<PiranhaBite>(), 900);

            if (target.TryGetGlobalNPC<PiranhaBiteArmor>(out PiranhaBiteArmor bite))
            {
                if (bite.biteStacks < PiranhaBiteArmor.BiteStackMax)
                    bite.biteStacks++;
            }
        }
    }
}
