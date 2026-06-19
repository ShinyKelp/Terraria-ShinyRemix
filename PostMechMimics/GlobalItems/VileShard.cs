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

namespace ShinyRemix.PostMechMimics.GlobalItems
{
    public class VileShard : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public int shardID = 0;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && entity.type == ItemID.CrystalVileShard;
        }
        public override void SetDefaults(Item entity)
        {
            entity.damage += 15;
            entity.useTime = entity.useAnimation = 21;
            entity.knockBack *= 1.5f;
            if (!ShinyUtils.TRAE)
                entity.mana = 17;
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shardID++;
            if (shardID > 100)
                shardID = 0;
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
