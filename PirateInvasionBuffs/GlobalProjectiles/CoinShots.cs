using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PirateInvasionBuffs.GlobalProjectiles
{
    public class CoinShots : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.CopperCoin || entity.type == ProjectileID.SilverCoin ||
                entity.type == ProjectileID.GoldCoin || entity.type == ProjectileID.PlatinumCoin;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.owner == Main.myPlayer && target.type != NPCID.TargetDummy)
            {
                int id = ItemID.None;
                int amount = 1;
                float rand = Main.rand.NextFloat();
                if(rand < 0.05f)
                {
                    switch (projectile.type)
                    {
                        case ProjectileID.CopperCoin: id = ItemID.CopperCoin; break;
                        case ProjectileID.SilverCoin: id = ItemID.SilverCoin; break;
                        case ProjectileID.GoldCoin: id = ItemID.GoldCoin; break;    
                        case ProjectileID.PlatinumCoin: id = ItemID.PlatinumCoin; break;
                        default: break;
                    }
                }
                else if(rand < 0.5f)
                {
                    switch (projectile.type)
                    {
                        case ProjectileID.CopperCoin: id = ItemID.CopperCoin; break;
                        case ProjectileID.SilverCoin: id = ItemID.CopperCoin; break;
                        case ProjectileID.GoldCoin: id = ItemID.SilverCoin; break;
                        case ProjectileID.PlatinumCoin: id = ItemID.GoldCoin; break;
                        default: break;
                    }
                    if (Main.rand.NextBool())
                    {
                        amount++;
                        if (Main.rand.NextBool())
                        {
                            amount++;
                            if (Main.rand.NextBool())
                            {
                                amount++;
                            }
                        }
                    }
                }
                if(id != ItemID.None)
                {
                    for(int i = 0; i < amount; i++)
                        Item.NewItem(projectile.GetSource_FromThis(), projectile.Center, id);
                }
            }
        }
    }
}
