using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class ExplosionHandler
    {
        public static List<Light> explosions = new List<Light>();

        public static void addExplosion(Light light)
        {
            explosions.Add(light);
            Game1.penumbra.Lights.Add(light);
        }

        public static void update()
        {
            for(int i = 0; i < explosions.Count; i++)
            {

                explosions[i].Scale -= new Vector2(20f, 20f);
                if(explosions[i].Scale.X < 20)
                {
                    explosions.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
