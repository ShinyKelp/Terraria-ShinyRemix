using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ShinyRemix.PirateInvasionBuffs
{
    public static class PirateInvasionIL
    {
        public static void ChangePirateInvasionCheck(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);

            c.GotoNext(MoveType.After,
                x => x.MatchLdsfld<Terraria.WorldGen>("altarCount")
                );
            c.EmitDelegate<Func<int, int>>((b) => {
                if (ShinyOptions.PirateInvasionBuffs && b > 0)
                    return NPC.downedMechBossAny ? 1 : 0;
                return b;
            });
        }
    }
}
