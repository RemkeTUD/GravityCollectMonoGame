using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    
    public class TextDialog
    {
        public static bool isInDialog = false;
        public List<Textbox> textboxes;
        public static int currentID = -1;
        public static TextDialog currentTextDialog;
        static KeyboardState state, prevstate;
        public TextDialog()
        {

        }
        public TextDialog(List<Textbox> textboxes)
        {
            this.textboxes = textboxes;
        }

        public static void setUpDialog(TextDialog dialog)
        {
            currentTextDialog = dialog;
            isInDialog = true;
            currentID = 0;
        }

        public static void nextID()
        {
            isInDialog = true;
            currentID++;
            if(currentID >= currentTextDialog.textboxes.Count)
            {
                currentID = -1;
                isInDialog = false;
            }
        }

        public static void update()
        {
            state = Keyboard.GetState();
            if(isInDialog && state.IsKeyDown(Keys.Space)&& prevstate.IsKeyUp(Keys.Space))
            {
                nextID();
                Game1.getPlayer().prevState = Keyboard.GetState();
            }
            prevstate = Keyboard.GetState();
        }

        public static void draw(SpriteBatch batch)
        {
            if(isInDialog)
                currentTextDialog.textboxes[currentID].Draw(batch);
            update();
        }

    }
}
