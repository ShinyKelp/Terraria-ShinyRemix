using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.VanillaSpears
{
    public class NorthPoleSnowflake : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.NorthPoleSnowflake;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projectile.damage = (int)Math.Floor(Main.player[projectile.owner].HeldItem.damage * 0.8f);
            base.OnSpawn(projectile, source);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 180);
        }
    }
}
