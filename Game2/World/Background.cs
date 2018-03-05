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
    [XmlInclude(typeof(List<BackgroundObject>))]
    [XmlInclude(typeof(BackgroundObject))]
    [XmlInclude(typeof(Vector2))]
    public class Background
    {
       public List<BackgroundObject> objects = new List<BackgroundObject>(); //Name zu /Position/Entfernung/Textur Tupel

        public Background()
        {

        }
        
        public void addObject(string name, Vector2 pos,float distance, string texPath)
        {
            objects.Add(new BackgroundObject(pos, distance, texPath));
           
        }

        
        public void draw(SpriteBatch batch)
        {
            foreach(BackgroundObject obj in objects)
            {
                obj.draw(batch);
                
                
            }
        }

    }
}
