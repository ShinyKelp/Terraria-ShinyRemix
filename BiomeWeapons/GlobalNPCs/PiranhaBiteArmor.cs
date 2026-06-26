using Microsoft.Xna.Framework;
using ShinyRemix.BiomeWeapons.ModBuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.GlobalNPCs
{
    public class PiranhaBiteArmor : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int biteStacks = 0;
        public const int BiteStackMax = 20;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return ShinyOptions.BiomeKeyWeapons;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if(modifiers.DamageType == DamageClass.Ranged)
                modifiers.ArmorPenetration += biteStacks;
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void ResetEffects(NPC npc)
        {
            if (!npc.HasBuff(ModContent.BuffType<PiranhaBite>()))
                biteStacks = 0;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (biteStacks == BiteStackMax)
            {
                int frequency = 10;
                float scale = 0.9f;
                if (npc.width > 48)
                {
                    frequency = 4;
                    scale = 1.2f;
                }
                if (Main.rand.Next(frequency) == 0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.CrimsonSpray, 0, 0, 0, default(Color), scale);
                }
                drawColor = Color.OrangeRed;
            }
        }
    }
}
