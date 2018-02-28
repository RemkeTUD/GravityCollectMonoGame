using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Background
    {
        Dictionary<string, Tuple<Vector2, int,Texture2D>> objects = new Dictionary<string, Tuple<Vector2, int, Texture2D>>(); //Name zu /Position/Entfernung/Textur Tupel

        public Background()
        {

        }
        
        public void addObject(string name, Vector2 pos,int distance, string texPath)
        {
            objects.Add(name, new Tuple<Vector2, int, Texture2D>(pos, distance, Game1.cManager.Load<Texture2D>(texPath)));
           
        }


        Rectangle rect = new Rectangle();
        public void draw(SpriteBatch batch)
        {
            foreach(Tuple<Vector2, int, Texture2D> obj in objects.Values)
            {
                
                rect.X = (int)(obj.Item1.X + (Game1.getCam().Pos.X * 0.1f) * obj.Item2);
                rect.Y = (int)(obj.Item1.Y + (Game1.getCam().Pos.Y * 0.1f) * obj.Item2);

                rect.Width = obj.Item3.Width;
                rect.Height = obj.Item3.Height;

                batch.Draw(obj.Item3,
                        destinationRectangle: rect,
                        
                        color: Color.White);
                
            }
        }

    }
}
