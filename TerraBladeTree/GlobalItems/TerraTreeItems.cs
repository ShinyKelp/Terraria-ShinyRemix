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
                    entity.damage += 35;    //85 > 120. Requires Golem + Empress. + Projectile deals 80% dmg (96)
                    break;
                case ItemID.LightsBane:
                    entity.damage -= 1;     //16 > 15
                    entity.crit += 11;      //Crits are cool with Light's Bane, increasing projectile size.
                                            //Trade 6% damage for 11% crit.
                    break;
                case ItemID.BloodButcherer:
                    entity.damage += 2;
                    entity.scale += 0.05f;  //Making the Butcherer a litle better.
                    break;
                case ItemID.TrueExcalibur:
                    entity.damage += 8;     //72 > 80. Is post-Plantera.
                    break;
                case ItemID.TrueNightsEdge: 
                    entity.crit += 6;       //Damage stays at 70, weaker vs armored opponents.
                                            //Requires blood moon and lost range.
                    break;
            }
        }
    }
}
