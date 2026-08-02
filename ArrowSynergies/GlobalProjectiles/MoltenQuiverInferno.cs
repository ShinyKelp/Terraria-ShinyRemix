using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ShinyRemix.ArrowSynergies.ArrowSynergyUtils;

namespace ShinyRemix.ArrowSynergies.GlobalProjectiles
{
    public class MoltenQuiverInferno : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool isMoltenArrow = false;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SimpleArrowCompatibility && (entity.type == ProjectileID.FireArrow || !(
                BowArrowSignatures.ContainsValue(entity.type)
                || ArrowOverrides.Contains(entity.type)));
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_ItemUse_WithAmmo ammoSource && ammoSource.Player.HeldItem.useAmmo == AmmoID.Arrow && ammoSource.Player.hasMoltenQuiver)
            {
                isMoltenArrow = true;
            }
            else if(source is EntitySource_Parent parentSource)
            {
                if (parentSource.Entity is Projectile parentProjectile && parentProjectile.TryGetGlobalProjectile<MoltenQuiverInferno>(out MoltenQuiverInferno parentQuiver) && parentQuiver.isMoltenArrow)
                    isMoltenArrow = true;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if(isMoltenArrow)
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);

            return true;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(isMoltenArrow)
                target.AddBuff(BuffID.OnFire3, 600);

        }
    }
}
