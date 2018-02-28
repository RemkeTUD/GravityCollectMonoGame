using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Ink : Item
    {
        Animation animation = new Animation(16, 16, 6, true, 4);
        int frame = 0;
        public Ink()
        {
            textureTest = Game1.cManager.Load<Texture2D>("ink");
            hasIllumination = true;
            illuminationStrength = 0.0015f;
        }

        public Ink(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("ink");
            hasIllumination = true;
            illuminationStrength = 0.0015f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            frame++;
            sourceRect = animation.getSourceRectange();
            animation.update();
            this.angle = (float)(Math.Sin(frame * 0.1f) * 0.1f) + MapTools.VectorToAngle(WorldInfo.gravity) - 3.1415f * 0.5f;
            r = 150; g = 150; b = 150;
            base.Draw(spriteBatch);
        }
    }
}
