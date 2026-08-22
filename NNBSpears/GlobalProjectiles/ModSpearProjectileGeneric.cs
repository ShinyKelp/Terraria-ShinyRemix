using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.NNBSpears.GlobalProjectiles
{
    //Class for all non-supported spears.
    public class ModSpearProjectileGeneric : SpearProjectileBase
    {
        private static Dictionary<int, KeyValuePair<float, float>> EstimatedSizes = new Dictionary<int, KeyValuePair<float, float>>();

        private int projType;
        protected override float HoldoutRangeMax => EstimatedSizes[projType].Key;
        protected override float HoldoutRangeMin => EstimatedSizes[projType].Value;

        protected override bool UsesCustomHitCooldown => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            if (ShinyOptions.SpearRework &&
                entity.aiStyle == ProjAIStyleID.Spear &&
                !NNBSpearUtils.VanillaSpears.Contains(entity.type) &&
                !NNBSpearUtils.ModSpearProjIDs.ContainsValue(entity.type))
                return true;
            else return false;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            projType = projectile.type;
            if (!EstimatedSizes.ContainsKey(projType))
            {
                AddEstimatedSize(projectile);
            }
            base.OnSpawn(projectile, source);
        }
        private void AddEstimatedSize(Projectile projectile)
        {
            //Comparing sprite width with Titanium trident (which is 110), and adjusting stats accordingly.
            float titanWidth = 110f;
            Texture2D modTex = TextureAssets.Projectile[projectile.type].Value;
            float modWidth = modTex.Width;
            float ratio = modWidth / titanWidth;
            float estimatedHoldoutMax = (float)Math.Round(ratio * 166f);
            float estimatedHoldoutMin = (float)Math.Round(ratio * 38f);
            EstimatedSizes.Add(projectile.type, new KeyValuePair<float, float>(estimatedHoldoutMax, estimatedHoldoutMin));
        }
    }
}
