using Microsoft.Xna.Framework;
using ShinyRemix.OverloadMechanic.GlobalItems;
using ShinyRemix.OverloadMechanic.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace ShinyRemix.OverloadMechanic.GlobalProjectiles
{
    public class OverloadedProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool IsDuplicate = false;
        bool needsCheck = true;
        IEntitySource source;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.OverloadMechanic && entity.DamageType == DamageClass.Ranged;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            this.source = source;

            base.OnSpawn(projectile, source);
        }
        public override void PostAI(Projectile projectile)
        {
            if (needsCheck)
            {
                needsCheck = false;
                if (!IsDuplicate)
                {
                    DoOverloadCheck(projectile);
                }
            }
        }

        void DoOverloadCheck(Projectile projectile)
        {
            if (projectile.owner == Main.myPlayer && source is EntitySource_ItemUse_WithAmmo ammoSource && projectile.Center.Distance(Main.player[projectile.owner].Center) < 48f)
            {
                Player player = Main.player[projectile.owner];
                if (ammoSource.Item.TryGetGlobalItem<OverloadedItem>(out OverloadedItem overIt) && overIt.overloaded)
                {
                    Item ammo = player.ChooseAmmo(ammoSource.Item);
                    if (ammo.type == ammoSource.AmmoItemIdUsed && player.CountItem(ammoSource.AmmoItemIdUsed) > 10)
                    {
                        bool successfulOverload = false;
                        if (IsCapableOfConsumingAmmo(projectile, ammo))
                        {
                            successfulOverload = player.IsAmmoFreeThisShot(player.HeldItem, ammo, player.HeldItem.shoot);
                        }
                        else
                        {
                            Item item = new Item();
                            item.SetDefaults(ItemID.WoodenBow);
                            item.shoot = player.HeldItem.shoot;
                            successfulOverload = player.IsAmmoFreeThisShot(item, ammo, item.shoot);
                        }

                        if (successfulOverload)
                        {
                           OverloadShot overShot = new OverloadShot();
                            overShot.PositionOffset = player.Center - projectile.Center;
                            Vector2 velocity = projectile.velocity;
                            velocity.X *= 1f + (Main.rand.NextFloat() * -0.05f);
                            velocity.Y *= 1f + (Main.rand.NextFloat() * -0.05f);
                            overShot.Velocity = velocity;
                            overShot.source = source;
                            overShot.Type = projectile.type;
                            overShot.Knockback = projectile.knockBack;
                            overShot.ai0 = projectile.ai[0];
                            overShot.ai1 = projectile.ai[1];
                            overShot.ai2 = projectile.ai[2];
                            overShot.AmmoUsed = ammo.type;
                            overShot.counter = 3;
                            if (Main.rand.NextBool())
                                overShot.counter++;
                            overShot.originalProjectile = projectile.whoAmI;

                            overShot.Damage = projectile.damage - (int)Math.Floor(0.5f * player.GetTotalDamage(player.HeldItem.DamageType).ApplyTo(player.HeldItem.damage));

                            if (player.active && !player.dead && player.TryGetModPlayer<ExtraShots>(out ExtraShots extraShots))
                            {
                                overShot.local0 = projectile.localAI[0];
                                overShot.local1 = projectile.localAI[1];
                                overShot.local2 = projectile.localAI[2];
                                extraShots.AddExtraShot(overShot);
                            }
                        }
                    }
                    overIt.lastShotFrames = 0;
                }

            }

        }

        //WORKS.
        private bool IsCapableOfConsumingAmmo(Projectile projectile, Item ammo)
        {
            Player player = Main.player[projectile.owner];
            bool foundConsume = false;
            int repetitions = 0;
            while(!foundConsume && repetitions < 30)
            {
                foundConsume = !player.IsAmmoFreeThisShot(player.HeldItem, ammo, player.HeldItem.shoot); //ItemLoader.CanConsumeAmmo(player.HeldItem, ammo, player);
                repetitions++;
            }
            return foundConsume;
        }
    }
}
