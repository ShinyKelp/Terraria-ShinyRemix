using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShinyRemix.MeteorArmorSynergies.ModPlayers
{
    public class FreeMeteorWeapons : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            if (ShinyOptions.MeteorArmorSynergies && Player.spaceGun)
            {
                Player.GetDamage(DamageClass.Magic) -= 0.05f;
            }
        }
        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (ShinyOptions.MeteorArmorSynergies && Player.spaceGun)
            {
                if (item.type == ItemID.FlowerofFire || item.type == ItemID.Flamelash 
                    || item.type == ItemID.BookofSkulls || item.type == ItemID.MeteorStaff)
                    mult = 0f;
            }
        }
    }
}
