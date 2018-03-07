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
            //r = 0; g = 0; b = 0;
            

            if (collidesLeftWithMap().getRealSpeed() > 0.01f)
                setRealSpeed(collidesLeftWithMap().getRealSpeed());
            else if (collidesRightWithMap().getRealSpeed() < -0.01f)
                setRealSpeed(collidesRightWithMap().getRealSpeed());
            else
                setRealSpeed(0);

            if (collidesUpWithMap().collided && getFallSpeed() < -0.1f)
            {
                setFallSpeed(collidesUpWithMap().getFallSpeed() + 1);
            }
            if (!collidesDownWithMap().collided) { 
                setFallSpeed(getFallSpeed() + 0.8f);
                if (getFallSpeed() > 17.5)
                    setFallSpeed(17.5f);
            }
            else
            {
                setFallSpeed(collidesDownWithMap().getFallSpeed());
                if (collidesDownWithMap().getRealSpeed() != 0)
                {
                    setRealSpeed(collidesDownWithMap().getRealSpeed());
                }

                

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

            foreach (Vector2 point in Game1.getPlayer().leftPoints())
            {
                if (collidesWithMovingPoint(point, Vector2.Zero))
                {
                    if (Game1.getPlayer().speed < -0.01f)
                        setRealSpeed(Game1.getPlayer().speed);
                    break;
                }
            }




            base.Update();
        }

        public void setSpeedChains()
        {
            
            if (getRealSpeed() > 0.1f)
            if (!setSpeedChainRight(getRealSpeed())) {
                if(Game1.getPlayer().speed > 0.01)
                    Game1.getPlayer().speed = 0;
                }
            if (getRealSpeed() < -0.1f)
                if (!setSpeedChainLeft(getRealSpeed()))
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
            
                if ((collidesRightWithMap().isStatic && speed > 0.1f) || (collidesLeftWithMap().isStatic && speed <-0.1f))
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
           
            if (speed > 0.01f) {
                g = 255;
                Console.WriteLine("Test1");
                Console.WriteLine(speed);
                if (collidesRightWithMap().isStatic)
                {
                    Console.WriteLine("Test2");
                    Console.WriteLine(this.speed);
                    //this.setRealSpeed(0);

                    
                    setSpeedChainUp(0);
                    return false;
                }
                Console.WriteLine("Test3");
                Console.WriteLine(speed);
                this.setRealSpeed(speed);
                setSpeedChainUp(speed);
                foreach (FreeGravityBox gravityBox in boxes)
                {
                    Console.WriteLine("Test4");
                    Console.WriteLine(this.speed);
                    if (gravityBox!=this)
                    if(gravityBox.collidesWithPoints(rightPoints()))
                    {
                        if(!gravityBox.setSpeedChainRight(speed))
                        {
                                Console.WriteLine("Test5");
                               // this.setRealSpeed(0);
                            setSpeedChainUp(0);
                            return false;
                        }
                        
                        
                    }
                }
            }
            Console.WriteLine(this.speed);
            return true;
        }

        public bool setSpeedChainLeft(float speed)
        {
            
            if (speed < -0.01f)
            {
                g = 128;
                if (collidesLeftWithMap().isStatic)
                {
                    //this.setRealSpeed(0);
                    setSpeedChainUp(0);
                    return false;

                }
                this.setRealSpeed(speed);
                setSpeedChainUp(speed);

                foreach (FreeGravityBox gravityBox in boxes)
                {
                    if (gravityBox != this)
                        if (gravityBox.collidesWithPoints(leftPoints()))
                    {
                        if (!gravityBox.setSpeedChainLeft(speed))
                        {
                            //this.setRealSpeed(0);
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
            if(getFallSpeed() > 0.01f) {
            while (collidesDownWithMap().collided)
            {
                pos.X -= 0.1f * (float)(WorldInfo.gravity.X);
                pos.Y -= 0.1f * (float)(WorldInfo.gravity.Y);
            }
            pos.X += 0.4f *(float)Math.Round(WorldInfo.gravity.X);
            pos.Y += 0.4f  * (float)Math.Round(WorldInfo.gravity.Y);

            rect.X = (int)Math.Round(pos.X);
            rect.Y = (int)Math.Round(pos.Y);
            }
        }

        public override void correctRightCollision()
        {
            if (getRealSpeed() > 0.1f)
            {
                if (collidesRightWithMap().collided)
            {
                while (collidesRightWithMap().collided)
                {
                    pos.X -= 0.1f * (float)(WorldInfo.gravity.Y);
                    pos.Y += 0.1f * (float)(WorldInfo.gravity.X);
                }
                pos.X += 0.5f *(float)Math.Round(WorldInfo.gravity.Y);
                pos.Y -= 0.5f *(float)Math.Round(WorldInfo.gravity.X);

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;
            }
             }
        }

        public override void correctLeftCollision()
        {
            if (getRealSpeed() < -0.1f)
            {
                if (collidesLeftWithMap().collided)
                {
                    while (collidesLeftWithMap().collided)
                    {
                        pos.X += 0.1f * (float)(WorldInfo.gravity.Y);
                        pos.Y -= 0.1f * (float)(WorldInfo.gravity.X);
                    }
                    pos.X -= 0.5f * (float)Math.Round(WorldInfo.gravity.Y);
                    pos.Y += 0.5f * (float)Math.Round(WorldInfo.gravity.X);

                    rect.X = (int)pos.X;
                    rect.Y = (int)pos.Y;
                }
            }
        }

    }
}
