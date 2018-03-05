using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Game1
{
    public class ParticleType
    {

        public static readonly ParticleType SPARK = new ParticleType("spark", 8, 8, 4);
        public static readonly ParticleType ENGINE = new ParticleType("engine", 16, 16, 7);


        private Texture2D texture;
        private int width;
        private int height;
        private int maxFrame;

        public ParticleType(string texName, int width, int height, int maxFrame)
        {
            texture = Game1.cManager.Load<Texture2D>(texName);
            this.width = width;
            this.height = height;
            this.maxFrame = maxFrame;
        }

        public static IEnumerable<ParticleType> Values
        {
            get
            {
                yield return SPARK;
                yield return ENGINE;
  
            }
        }

        public Texture2D Texture { get { return texture; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int MaxFrame { get { return maxFrame; } }
    }
}
