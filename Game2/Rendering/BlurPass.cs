using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class BlurPass
    {
        private static Effect horBlur;
        private static Effect vertBlur;

        private int width = 0;
        private int height = 0;
        private float factor = 1.0f;

        private RenderTarget2D blurTarget;
        private GraphicsDevice GraphicsDevice;

        public BlurPass(GraphicsDevice GraphicsDevice, ContentManager Content, int width, int height, float factor)
        {
            if(horBlur == null)
                horBlur = Content.Load<Effect>("shader/horBlur");
            if(vertBlur == null)
                vertBlur = Content.Load<Effect>("shader/vertBlur");

            this.width = width;
            this.height = height;
            this.factor = factor;
            this.GraphicsDevice = GraphicsDevice;
            blurTarget = new RenderTarget2D(GraphicsDevice, width, height);

        }

        public void blur(SpriteBatch spriteBatch, Texture2D source, RenderTarget2D target)
        {
            horBlur.Parameters["resolution"].SetValue((float)width);
            horBlur.Parameters["factor"].SetValue(factor);
            vertBlur.Parameters["resolution"].SetValue((float)height);
            vertBlur.Parameters["factor"].SetValue(factor);

            GraphicsDevice.SetRenderTarget(blurTarget);

            spriteBatch.Begin(0, null, SamplerState.LinearClamp, null, null, horBlur);
            spriteBatch.Draw(source, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(target);

            spriteBatch.Begin(0, null, SamplerState.LinearClamp, null, null, vertBlur);
            spriteBatch.Draw(blurTarget, new Rectangle(0, 0, target.Width, target.Height), Color.White);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }
    }
}
