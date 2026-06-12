using Microsoft.Xna.Framework;
using ShinyRemix.BlessedMechanic.Buffs;
using ShinyRemix.BlessedMechanic.GlobalItems;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.GlobalProjectiles
{
    public class BlessedProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool blessed = false;
        const int buffTimerPerHit = 30;
        const int maxBuffTimer = 630;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.BlessedMechanic && (entity.DamageType == DamageClass.Magic || 
                entity.type == ProjectileID.BloodCloudMoving || entity.type == ProjectileID.BloodCloudRaining
                || entity.type == ProjectileID.RainCloudMoving || entity.type == ProjectileID.RainCloudRaining);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse itemSource &&
                itemSource.Item.TryGetGlobalItem(out BlessedItem blessedItem) &&
                blessedItem.blessed)
            {
                blessed = true;
            }
            else if (source is EntitySource_Parent parentSource &&
                parentSource.Entity is Projectile parentProjectile)
            {
                if(parentProjectile.TryGetGlobalProjectile(out BlessedProjectile parentGlobal) && parentGlobal.blessed)
                    blessed = true;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!blessed)
                return;
            if (target.type == NPCID.TargetDummy)
                return;
            
            Player owner = Main.player[projectile.owner];
            Vector2 distance = owner.position - target.position;
            if (distance.Length() > 1400)
                return;

            if(owner.HeldItem != null && owner.HeldItem.TryGetGlobalItem(out BlessedItem blessedItem) &&
                blessedItem.blessed)
            {
                int buffType = ModContent.BuffType<BlessedBuff>();
                int buffIndex = owner.FindBuffIndex(buffType);
                if (buffIndex != -1)
                    owner.buffTime[buffIndex] = (int)Math.Min(owner.buffTime[buffIndex] + buffTimerPerHit, maxBuffTimer);
                else
                    owner.AddBuff(buffType, buffTimerPerHit);
                int regenIndex = owner.FindBuffIndex(BuffID.ManaRegeneration);
                if (regenIndex != -1)
                    owner.buffTime[regenIndex] = 0;
            }
        }
    }
}
