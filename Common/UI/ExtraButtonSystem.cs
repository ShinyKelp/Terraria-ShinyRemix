using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace ShinyRemix.Common.UI
{
    //Generic class to add an extra button to a vanilla NPC.
    public class ExtraButtonSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShinyRemix: NPC Extra Buttons",
                    DrawCustomButton,
                    InterfaceScaleType.UI)
                );
            }
        }

        protected virtual string ButtonText => "Button";
        protected virtual int NPC_ID => -1;

        private bool previousMouseLeft = false;

        //Function drawing an extra button over NPC chat UI. Mimics vanilla looks.
        private bool DrawCustomButton()
        {
            // Must be talking to an NPC
            if (Main.LocalPlayer.talkNPC < 0)
                return true;

            if (string.IsNullOrEmpty(Main.npcChatText))
                return true;

            NPC npc = Main.npc[Main.LocalPlayer.talkNPC];

            if (npc.type != NPC_ID)
                return true;

            ChatManager.ParseMessage(
                Main.npcChatText,
                Color.White
            );

            string[] wrappedLines = Utils.WordwrapString(
                Main.npcChatText,
                FontAssets.MouseText.Value,
                460,
                10,
                out int amountOfLines);

            // Positioning taken (mostly) from vanilla code
            float buttonX = 440 + (Main.screenWidth - 800) / 2;
            float buttonY = 130 + (amountOfLines+1) * 30;

            Vector2 position = new Vector2(buttonX, buttonY);
            string text = ButtonText;

            // Measure text for hover hitbox
            Vector2 textSize = FontAssets.MouseText.Value.MeasureString(text);

            Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);

            Vector2 mouse = Main.MouseScreen;
            bool hovering = hitbox.Contains(mouse.ToPoint());

            //Values taken from vanilla code
            int mouseColor = Main.mouseTextColor;
            Color textColor = new Color(mouseColor, (int)(mouseColor / 1.1f) ,mouseColor / 2, mouseColor);
            Color borderColor = hovering? Color.Brown : Color.Black;
            Vector2 scale = hovering ? new Vector2(1.2f) : new Vector2(0.9f);
            Vector2 drawPosition = position + textSize * 0.5f;

            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                FontAssets.MouseText.Value,
                text,
                drawPosition,
                textColor,
                borderColor,
                0f,
                textSize * 0.5f,
                scale,
                -1f,
                2f
            );

            // Click
            // We don't check Main.mouseLeftRelease, because due to the interface layer,
            // vanilla might have already consumed that variable
            if (hovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && !previousMouseLeft)
                {
                    Main.mouseLeftRelease = false;
                    OnButtonPressed();
                }
            }
            previousMouseLeft = Main.mouseLeft;
            return true;
        }

        protected virtual void OnButtonPressed()
        {

        }
    }
}
