using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ShinyRemix.BlessedMechanic.Buffs
{
    public class BlessedPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<BlessedBuff>()))
            {
                Player.manaCost *= 0.2f;
                Player.manaRegenBuff = true;
                if (ShinyUtils.TRAE)
                {
                    //Kind of ugly compatibility with TRAE here, we do reflection to access their manaRegenBoost field and increase it drastically
                    //Should probably just recreate mana regen separately
                    if (ModLoader.TryGetMod("TRAEProject", out Mod trae))
                    {
                        Type manaType = trae.Code.GetType("TRAEProject.Changes.Mana");

                        FieldInfo modPlayersField = typeof(Player).GetField("modPlayers", BindingFlags.Instance | BindingFlags.NonPublic);

                        var modPlayers = (IList<ModPlayer>)modPlayersField.GetValue(Main.LocalPlayer);

                        ModPlayer manaPlayer = modPlayers.FirstOrDefault(mp => mp.GetType() == manaType);

                        FieldInfo boostField = manaType.GetField("manaRegenBoost");

                        float current = (float)boostField.GetValue(manaPlayer);

                        boostField.SetValue(manaPlayer, current + 4f);
                    }
                }
            }
        }
    }
}
