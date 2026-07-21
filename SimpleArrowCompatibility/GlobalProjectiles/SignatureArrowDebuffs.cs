using Microsoft.Xna.Framework;
using ShinyRemix.SimpleArrowCompatibility.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ShinyRemix.SimpleArrowCompatibility.SimpleArrowCompatUtils;
namespace ShinyRemix.SimpleArrowCompatibility.GlobalProjectiles
{
    public class SignatureArrowDebuffs : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SimpleArrowCompatibility && (
                BowArrowSignatures.ContainsValue(entity.type)
                || ArrowOverrides.Contains(entity.type));
        }

        public int debuffType = -1;
        const int debuffTime = 600;
        public bool MoltenQuiverDebuff = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            if (player.HeldItem.TryGetGlobalItem<BowsWithSignatures>(out BowsWithSignatures globalItem))
                debuffType = globalItem.debuffType;
            else if (source is EntitySource_ItemUse_WithAmmo ammoSource && ArrowItemDebuffs.ContainsKey(ammoSource.AmmoItemIdUsed))
                debuffType = ArrowItemDebuffs[ammoSource.AmmoItemIdUsed];
            if (player.hasMoltenQuiver)
            {
                MoltenQuiverDebuff = true;
            }
            base.OnSpawn(projectile, source);
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.FairyQueenRangedItemShot)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            base.AI(projectile);
        }

        public override void PostAI(Projectile projectile)
        {
            switch (debuffType)
            {
                case -1:
                    break;
                case BuffID.OnFire:
                    FlamingArrowDust(projectile);
                    break;
                case BuffID.Frostburn:
                case BuffID.Frostburn2:
                    FrostArrowDust(projectile);
                    break;
                case BuffID.CursedInferno:
                    CursedArrowDust(projectile);
                    break;
                case BuffID.Ichor:
                    IchorArrowDust(projectile); 
                    break;
                case BuffID.Venom:
                    VenomArrowDust(projectile);
                    break;
                default: break;
            }
            if (MoltenQuiverDebuff && debuffType != BuffID.OnFire)
                FlamingArrowDust(projectile);
            base.PostAI(projectile);
        }
        private void FlamingArrowDust(Projectile projectile)
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
        }

        private void FrostArrowDust(Projectile projectile)
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
        }

        private void CursedArrowDust(Projectile projectile)
        {
            int num17 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
            if (Main.rand.Next(2) == 0)
            {
                Main.dust[num17].noGravity = true;
                Main.dust[num17].scale *= 2f;
            }
        }

        private void IchorArrowDust(Projectile projectile)
        {
            int num18 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 169, 0f, 0f, 100, default(Color), 1f);
            if (Main.rand.Next(2) == 0)
            {
                Main.dust[num18].noGravity = true;
                Main.dust[num18].scale *= 1.5f;
            }
        }

        private void VenomArrowDust(Projectile projectile)
        {
            int num67 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100, default(Color), 1f);
            Main.dust[num67].scale = (float)Main.rand.Next(1, 10) * 0.1f;
            Main.dust[num67].noGravity = true;
            Main.dust[num67].fadeIn = 1.5f;
            Main.dust[num67].velocity *= 0.25f;
            Main.dust[num67].velocity += projectile.velocity * 0.25f;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (MoltenQuiverDebuff)
                target.AddBuff(BuffID.OnFire3, debuffTime);

            if (debuffType >= 0)
            {
                if(!MoltenQuiverDebuff || debuffType != BuffID.OnFire)
                    target.AddBuff(debuffType, debuffTime);
            }

            //Move elsewhere?
            if(projectile.type == ProjectileID.FrostArrow)
            {
                target.AddBuff(BuffID.Frostburn2, 600);
            }
            else if(projectile.type == ProjectileID.BloodArrow) //Not relevant until 1.4.5
            {
                target.AddBuff(BuffID.Bleeding, 600);
            }
        }
    }
}

