using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class PlayerPositionShow
    {
        Texture2D textureTest;
        Vector2 pos, size;
        bool flipped;
        Rectangle rect;
        Vector2 gravity;
        public PlayerPositionShow(Vector2 pos, Vector2 size, bool flipped, Vector2 gravity)
        {
            textureTest = Game1.cManager.Load<Texture2D>("player");
            this.pos = pos;
            this.size = size;
            this.flipped = flipped;
            rect = new Rectangle(0,0, 16,32);
            this.gravity = gravity;
        }

        public void Draw(SpriteBatch spriteBatch, int id)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            SpriteEffects effect;
            if (flipped)
                effect = SpriteEffects.FlipHorizontally;
            else
                effect = SpriteEffects.None;
            spriteBatch.Draw(textureTest, destinationRectangle: rect, color: new Color(0,0,0,1f - (id * (1f/(30f)))), rotation: MapTools.VectorToAngle(gravity) - (float)Math.PI * 0.5f, origin: new Vector2(textureTest.Width * 0.5f, textureTest.Height * 0.5f), effects: effect);

            

        }
    }
}
