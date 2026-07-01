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

        public override bool PreAI(Projectile projectile)
        {
            if(projectile.owner == Main.myPlayer && projectile.ai[1] != -1f)
            {
                Player player = Main.player[projectile.owner];
                if (player.Distance(projectile.position) > 640f)
                {
                    projectile.ai[1] = -1f;
                }
            }
            return base.PreAI(projectile);
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
