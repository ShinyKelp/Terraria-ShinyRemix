using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.BiomeWeapons.ModBuffs
{
    public class PiranhaBite : ModBuff
    {
        public int stack = 0;
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
        }

    }
}
