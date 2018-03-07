using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game1
{
    
    [XmlInclude(typeof(Vector2))]
    public class BackgroundObject
    {
        public Vector2 pos;
        public float distance;
        public string texPath;
        public Vector2 speed;
        Texture2D tex;

        public BackgroundObject()
        {

        }
        public BackgroundObject(Vector2 pos, float distance, string texPath, Vector2 speed)
        {
            this.pos = pos;
            this.distance = distance;
            this.texPath = texPath;
            this.speed = speed;

        }
        Rectangle rect = new Rectangle();
        public void draw(SpriteBatch batch)
        {
            if (tex == null)
                tex = Game1.cManager.Load<Texture2D>(texPath);
            rect.X = (int)(pos.X + ((Game1.getCam().Pos.X - 1000) * 0.1f) * distance);
            rect.Y = (int)(pos.Y + ((Game1.getCam().Pos.Y - 1000) * 0.1f) * distance);

            rect.Width = tex.Width;
            rect.Height = tex.Height;

            pos += speed;
            if (pos.X > Game1.world.width * 16 || pos.X < rect.Width)
                pos.X %= Game1.world.width * 16;
            if (pos.Y > Game1.world.height * 16 || pos.Y < rect.Height)
                pos.Y %= Game1.world.height * 16;

            

            

            batch.Draw(tex,
                    destinationRectangle: rect,

                    color: Color.White);
        }

    }
}
