using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class FreeGravityBox : CollisionBox
    {

        public static List<FreeGravityBox> boxes = new List<FreeGravityBox>();
        public FreeGravityBox()
        {
            boxes.Add(this);
        }
        public FreeGravityBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            boxes.Add(this);
        }

        public override void Update()
        {
            r = 0; g = 0; b = 0;
            //if (!collidesLeftWithMap().collided && !collidesRightWithMap().collided)
            setRealSpeed(0);

            

            if (collidesUpWithMap().collided && getFallSpeed() < -0.1f)
            {
                setFallSpeed(collidesUpWithMap().getFallSpeed() + 1);
            }
            if (!collidesDownWithMap().collided)
                setFallSpeed(getFallSpeed() + 0.8f);
            else
            {
                setFallSpeed(collidesDownWithMap().getFallSpeed());
                if (collidesDownWithMap().getRealSpeed() != 0)
                {
                    setRealSpeed(collidesDownWithMap().getRealSpeed());
                }

                foreach (Vector2 point in Game1.getPlayer().rightPoints())
                {
                    if (collidesWithMovingPoint(point, Vector2.Zero))
                    {
                        if (Game1.getPlayer().speed > 0.01f)
                        {

                            setRealSpeed(Game1.getPlayer().speed);
                        }
                        break;
                    }
                }

                foreach (Vector2 point in Game1.getPlayer().leftPoints()) {
                    if (collidesWithMovingPoint(point, Vector2.Zero))
                    {
                        if (Game1.getPlayer().speed < -0.01f)
                            setRealSpeed(Game1.getPlayer().speed);
                        break;
                    }
                }

            }


            




            base.Update();
        }

        public void setSpeedChains()
        {

            if (!setSpeedChainRight(getRealSpeed())) {
                if(Game1.getPlayer().speed > 0.01)
                    Game1.getPlayer().speed = 0;
                }
            if(!setSpeedChainLeft(getRealSpeed()))
            {
                if (Game1.getPlayer().speed < -0.01)
                    Game1.getPlayer().speed = 0;
            }
        }

        public static void setAllSpeedChains()
        {
            foreach(FreeGravityBox box in boxes)
            {
                box.setSpeedChains();
            }
        }

        public override void checkForWalls()
        {
            if (collidesLeftWithMap().collided)
            {
                if (collidesLeftWithMap().getRealSpeed() > 0.1f)
                {
                    setRealSpeed(collidesLeftWithMap().getRealSpeed());
                }
                //if (getRealSpeed() < -0.01f)
                //    setRealSpeed(collidesLeftWithMap().getRealSpeed());
            }
            if (collidesRightWithMap().collided)
            {

                if (collidesRightWithMap().getRealSpeed() < -0.1f)
                {
                    setRealSpeed(collidesRightWithMap().getRealSpeed());
                }
               // if (getRealSpeed() > 0.01f)
                //    setRealSpeed(collidesLeftWithMap().getRealSpeed());
            }
        }

        public bool setSpeedChainUp(float speed)
        {
            r = 255;
            
                if (collidesRightWithMap().isStatic || collidesLeftWithMap().isStatic)
                {
                    this.setRealSpeed(0);
                    return false;
                }
                this.setRealSpeed(speed);
                foreach (FreeGravityBox gravityBox in boxes)
                {
                if (gravityBox != this)
                    foreach (Vector2 point in upPoints())
                    if (gravityBox.collidesWithMovingPoint(point, Vector2.Zero))
                    {
                        gravityBox.setSpeedChainUp(speed);
                        
                    }
                }
            
            return true;
        }

        public bool setSpeedChainRight(float speed)
        {
            
            if(speed > 0.01f) {
                g = 255;
                if (collidesRightWithMap().isStatic)
                {
                    this.setRealSpeed(0);
                    setSpeedChainUp(0);
                    return false;
                }
                this.setRealSpeed(speed);
                setSpeedChainUp(speed);
                foreach (FreeGravityBox gravityBox in boxes)
                {
                    //if(gravityBox!=this)
                    if(gravityBox.collidesWithMovingPoint(rightPoint(),Vector2.Zero))
                    {
                        if(!gravityBox.setSpeedChainRight(speed))
                        {
                            this.setRealSpeed(0);
                            setSpeedChainUp(0);
                            return false;
                        }
                        
                        
                    }
                }
            }
            return true;
        }

        public bool setSpeedChainLeft(float speed)
        {
            
            if (speed < -0.01f)
            {
                g = 128;
                if (collidesLeftWithMap().isStatic)
                {
                    this.setRealSpeed(0);
                    setSpeedChainUp(0);
                    return false;

                }
                this.setRealSpeed(speed);
                setSpeedChainUp(speed);

                foreach (FreeGravityBox gravityBox in boxes)
                {
                    //if (gravityBox != this)
                        if (gravityBox.collidesWithMovingPoint(leftPoint(), Vector2.Zero))
                    {
                        if (!gravityBox.setSpeedChainLeft(speed))
                        {
                            this.setRealSpeed(0);
                            setSpeedChainUp(0);
                            return false;
                        }
                        
                        
                    }
                }
            }
            return true;
        }

        public override void correctDownCollision()
        {
            while (collidesDownWithMap().collided)
            {
                pos.X -= (float)(WorldInfo.gravity.X);
                pos.Y -= (float)(WorldInfo.gravity.Y);
            }
            pos.X += 1*(float)Math.Round(WorldInfo.gravity.X);
            pos.Y += 1*(float)Math.Round(WorldInfo.gravity.Y);

            rect.X = (int)Math.Round(pos.X);
            rect.Y = (int)Math.Round(pos.Y);
        }

        public override void correctRightCollision()
        {
            if (collidesRightWithMap().collided)
            {
                while (collidesRightWithMap().collided)
                {
                    pos.X -= (float)(WorldInfo.gravity.Y);
                    pos.Y += (float)(WorldInfo.gravity.X);
                }
                pos.X += 2*(float)Math.Round(WorldInfo.gravity.Y);
                pos.Y -= 2*(float)Math.Round(WorldInfo.gravity.X);

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;
            }
        }

        public override void correctLeftCollision()
        {
            if (collidesLeftWithMap().collided)
            {
                while (collidesLeftWithMap().collided)
                {
                    pos.X += (float)(WorldInfo.gravity.Y);
                    pos.Y -= (float)(WorldInfo.gravity.X);
                }
                pos.X -= 2*(float)Math.Round(WorldInfo.gravity.Y);
                pos.Y += 2*(float)Math.Round(WorldInfo.gravity.X);

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;
            }
        }

    }
}
