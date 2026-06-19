using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.PostMechMimics.GlobalNPCs
{
    public class StrongerMimics : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return ShinyOptions.PostMechMimics && (entity.type == NPCID.BigMimicCorruption || entity.type == NPCID.BigMimicCrimson ||
               entity.type == NPCID.BigMimicHallow || entity.type == NPCID.BigMimicJungle);
        }
        int origDefense = -1;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
            origDefense = npc.defense;
        }
        public override void AI(NPC npc)
        {
            if( npc.Center.Y / 16f > Main.worldSurface)
                npc.defense = origDefense + 10;
            else
                npc.defense = origDefense;
            base.AI(npc);
        }
    }
}
