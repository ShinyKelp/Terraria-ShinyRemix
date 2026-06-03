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

namespace ShinyRemix.TerraBladeTree.GlobalItems
{
    public class TerraTreeItems : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ShinyOptions.TerraBladeTree && (entity.type == ItemID.TerraBlade || entity.type == ItemID.BladeofGrass || entity.type == ItemID.TrueExcalibur || entity.type == ItemID.TrueNightsEdge);
        }
        public override void SetDefaults(Item entity)
        {
            switch (entity.type)
            {
                case ItemID.TerraBlade:
                    entity.damage = 120;    //85 > 120. Requires Golem + Empress. + Projectile deals 80% dmg (96)
                    break;
                case ItemID.BladeofGrass:
                    entity.useAnimation = 30;
                    entity.useTime = 30;    //20 > 30.
                    entity.damage = 36;     //18 > 36.
                                            //Projectile damage: 4 > 9.
                                            //Is post-Bee.
                    break;
                case ItemID.TrueExcalibur:
                    entity.damage = 80;     //72 > 80. Is post-Plantera.
                    break;
                case ItemID.TrueNightsEdge: 
                    entity.crit += 6;       //Damage stays at 70, weaker vs armored opponents.
                                            //Requires blood moon and lost range.
                    break;
            }
        }
    }
}
