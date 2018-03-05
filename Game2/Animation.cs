using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Animation
    {
        int currentFrame;
        int maxFrame;
        int width, height;
        bool loop, play;
        int frameSkip;
        int totalFrames;

        public Animation(int width, int height, int maxFrame, bool loop, int frameSkip = 0)
        {
            this.width = width;
            this.height = height;
            this.maxFrame = maxFrame;
            this.loop = loop;
            this.frameSkip = frameSkip;
        }

        public void update()
        {

            if ((loop || play) && totalFrames % frameSkip == 0 && totalFrames != 0) {
                currentFrame += 1;
                currentFrame %= maxFrame;
                if (currentFrame == 0)
                    play = false;
            }
            totalFrames++;
            
        }
        public void startPlay()
        {
            play = true;
        }

        public Rectangle getSourceRectange()
        {
            Rectangle rect = new Rectangle(new Point(width * currentFrame, 0),new Point(width, height));
            return rect;
        }

    }
}
