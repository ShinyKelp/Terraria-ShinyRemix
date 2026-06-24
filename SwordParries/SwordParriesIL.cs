using Mono.Cecil.Cil;
using MonoMod.Cil;
using ShinyRemix.SwordParries.GlobalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ShinyRemix.SwordParries
{
    internal static class SwordParriesIL
    {
        internal static void EnableSwordParries(ILContext il)
        {
            ILCursor c = new(il);
            //Might have to be revised with version changes. Should be a relatively safe approach for now.
            c.GotoNext(MoveType.After,
                x => x.MatchStloc(2));
            c.Emit(OpCodes.Ldloc_2);
            c.Emit(OpCodes.Ldarg_0);

            c.EmitDelegate<Func<bool, Player, bool>>((orig_flag, player) => {
                if(player.HeldItem.TryGetGlobalItem<ParrySwords>(out ParrySwords parry) && parry.IsParrySword)
                    orig_flag = true;
                return orig_flag; 
            });

            c.Emit(OpCodes.Stloc_2);
        }
    }
}
