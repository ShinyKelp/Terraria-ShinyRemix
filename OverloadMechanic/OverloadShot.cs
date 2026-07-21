using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace ShinyRemix.OverloadMechanic
{
    public class OverloadShot
    {
        public Vector2 PositionOffset;
        public Vector2 Velocity;
        public IEntitySource source;
        public int Type;
        public int Damage;
        public float Knockback;
        public float ai0, ai1, ai2;
        public float local0, local1, local2;
        public int AmmoUsed;
        public int counter;
        public int originalProjectile;
    }
}
