using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShinyRemix.NNBSpears.GlobalProjectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles.StormAdditionsSpears
{
    public class CursedSpearGunProjectile : ModSpearProjectileBase
    {
        protected override float HoldoutRangeMax => 108f;
        protected override float HoldoutRangeMin => 10f;
        protected override float HoldPositionRelative => 0.78f;
        protected override string ModSpearName => "CursedSpearGunProj";

    }
}
