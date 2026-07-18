using Microsoft.Xna.Framework;
using ShinyRemix.OOAChanges.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace ShinyRemix.OOAChanges.GlobalItems
{
    public class BrandBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.OldOneArmyBuffs && entity.type == ItemID.DD2SquireDemonSword;
        }
        public override void SetDefaults(Item entity)
        {
            entity.useTime += 10;
            entity.useAnimation += 10;
            entity.damage += 50;
            entity.scale += 0.1f;
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<ParryBuffTracker>().isStrikeReady)
            {
                player.GetModPlayer<ParryBuffTracker>().isStrikeReady = false;
                Projectile.NewProjectile(item.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, -1f * player.gravDir, ProjectileID.Volcano, (int)item.damage * 5, item.knockBack, player.whoAmI, 0f, 2, 0f);
            }
        }
    }
}
