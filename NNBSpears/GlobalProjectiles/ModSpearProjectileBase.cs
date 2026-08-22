using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles
{
    public class ModSpearProjectileBase : SpearProjectileBase
    {
        protected virtual string ModSpearName => "";
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return ShinyOptions.SpearRework && NNBSpearUtils.ModSpearProjIDs.ContainsKey(ModSpearName) && NNBSpearUtils.ModSpearProjIDs[ModSpearName] != 1
                && NNBSpearUtils.ModSpearProjIDs[ModSpearName] == entity.type;
        }
    }
}
