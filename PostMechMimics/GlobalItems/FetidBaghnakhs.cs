using ShinyRemix.PostMechMimics.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.GlobalItems
{
    public class FetidBaghnakhs : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ItemID.FetidBaghnakhs;
        }
        public override void SetDefaults(Item entity)
        {
            entity.damage += 7;
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.GetModPlayer<FetidDefense>().defenseTimer = 120;
        }

    }
}
