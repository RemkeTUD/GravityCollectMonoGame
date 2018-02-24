using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Mipmap
    {
        private RenderTarget2D[] targetArray;
        private GraphicsDevice GraphicsDevice;
        private int levelCount = 0;
        private Texture2D source;
        public Mipmap(Texture2D texture, int levelCount, GraphicsDevice GraphicsDevice)
        {
            this.source = texture;
            this.GraphicsDevice = GraphicsDevice;
            this.levelCount = levelCount;
            targetArray = new RenderTarget2D[levelCount];
            int width = texture.Width;
            int height = texture.Height;
            for(int i = 0; i < levelCount; i++)
            {
                width /= 2;
                height /= 2;
                targetArray[i] = new RenderTarget2D(GraphicsDevice, width, height);
            }
        }

        public void generate(SpriteBatch spriteBatch)
        {
            GraphicsDevice.SetRenderTarget(targetArray[0]);
            spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
            spriteBatch.Draw(source, new Rectangle(0, 0, targetArray[0].Width, targetArray[0].Height), Color.White);
            spriteBatch.End();

            for (int i = 1; i < levelCount; i++)
            {
                GraphicsDevice.SetRenderTarget(targetArray[i]);
                spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
                spriteBatch.Draw(targetArray[i-1], new Rectangle(0, 0, targetArray[i].Width, targetArray[i].Height), Color.White);
                spriteBatch.End();
            }
            GraphicsDevice.SetRenderTarget(null);
        }

        public RenderTarget2D getLevel(int i)
        {
            return targetArray[i];

        }
    }
}
