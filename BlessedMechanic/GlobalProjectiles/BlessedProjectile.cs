using ShinyRemix.BlessedMechanic.Buffs;
using ShinyRemix.BlessedMechanic.GlobalItems;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.GlobalProjectiles
{
    public class BlessedProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool blessed = false;
        const int buffTimerPerHit = 30;
        const int maxBuffTimer = 630;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse itemSource &&
                itemSource.Item.TryGetGlobalItem(out BlessedItem blessedItem) &&
                blessedItem.blessed)
            {
                projectile.GetGlobalProjectile<BlessedProjectile>().blessed = true;
            }
            else if (source is EntitySource_Parent parentSource &&
                parentSource.Entity is Projectile parentProjectile)
            {
                var parentGlobal = parentProjectile.GetGlobalProjectile<BlessedProjectile>();
                if (parentGlobal.blessed)
                    projectile.GetGlobalProjectile<BlessedProjectile>().blessed = true;
                
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!blessed)
                return;

            Player owner = Main.player[projectile.owner];
            int buffType = ModContent.BuffType<BlessedBuff>();
            int buffIndex = owner.FindBuffIndex(buffType);
            if (buffIndex != -1)
                owner.buffTime[buffIndex] = (int)Math.Min(owner.buffTime[buffIndex] + buffTimerPerHit, maxBuffTimer);
            else
                owner.AddBuff(buffType, buffTimerPerHit);
        }
    }
}
