using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.Flails.ModProjectiles
{
    public class FlailExtensionProj: ModProjectile
    {
        public override string Texture => "Terraria/Images/Extra_0";


        public Terraria.Projectile parentProj;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 3600;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.width = 64;
            Projectile.height = 64;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.alpha = 255;
            parentProj = Main.projectile[(int)Projectile.ai[0]];
            base.OnSpawn(source);
        }

        public override void AI()
        {
            if(parentProj == null || !parentProj.active || parentProj.ai[0] != 0f)
            {
                Projectile.Kill();
                return;
            }
            Projectile.position = parentProj.position;

            //Set direction and velocity for proper knockback
            Projectile.direction = Math.Sign(Projectile.Center.X - Main.player[Projectile.owner].Center.X);
            Projectile.velocity.X = Projectile.direction;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (parentProj.localNPCImmunity[target.whoAmI] == 0)
                return base.CanHitNPC(target);
            else return false;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.localNPCImmunity[target.whoAmI] = parentProj.localNPCHitCooldown;
            parentProj.localNPCImmunity[target.whoAmI] = parentProj.localNPCHitCooldown;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = parentProj.Hitbox;
        }
    }
}
