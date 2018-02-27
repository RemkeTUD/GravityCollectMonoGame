using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class StaticEnemy : Enemy
    {
        Animation animation = new Animation(32, 32, 4, true, 8);
        bool flipped = true;
        public StaticEnemy()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/snake");
            animation.startPlay();
        }

        public StaticEnemy(ContentManager content, float X, float Y, float width, float height) : base(content, X, Y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/snake");
            animation.startPlay();
        }
        public override void Update()
        {
            if (CollidesWithPlayer())
                Game1.getPlayer().die();
            if (speed.X == 0 && speed.Y == 0)
            setUpDirection();

            if (speed.Y != 0)
            {
                if ((getBlockSetArray()[0,0] && getBlockSetArray()[1,0]) || (!getBlockSetArray()[0,0] && !getBlockSetArray()[1,0]))
                    speed.Y = 1;
                if ((getBlockSetArray()[0,1] && getBlockSetArray()[1,1]) || (!getBlockSetArray()[0,1] && !getBlockSetArray()[1,1]))
                    speed.Y = -1;
            }

            if (speed.X != 0)
            {
                if ((getBlockSetArray()[0,0] && getBlockSetArray()[0,1]) || (!getBlockSetArray()[0,0] && !getBlockSetArray()[0,1])) {
                    speed.X = 1;
                    flipped = false;
                }
                if ((getBlockSetArray()[1,0] && getBlockSetArray()[1,1]) || (!getBlockSetArray()[1,0] && !getBlockSetArray()[1,1])) {
                    speed.X = -1;
                    flipped = true;
                }
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sourceRect = animation.getSourceRectange();
            animation.update();
            //rect = new Rectangle((int)(pos.X - size.X * 0.5f), (int)(pos.Y + size.Y),(int)(size.X), (int)(size.X));
            SpriteEffects effect;
            if (flipped)
                effect = SpriteEffects.FlipHorizontally;
            else
                effect = SpriteEffects.None;
            spriteBatch.Draw(
                    textureTest,
                    position: pos + new Vector2(0, -size.Y * 0.5f),
                    sourceRectangle: sourceRect,
                    color: new Color(r, g, b, alpha),
                    rotation: angle,
                    origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                    scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                    effects: effect,
                    layerDepth: 1);
        
        
    }
    }
}
