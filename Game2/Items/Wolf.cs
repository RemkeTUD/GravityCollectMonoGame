﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Wolf : Item
    {
        public Wolf()
        {
            textureTest = Game1.cManager.Load<Texture2D>("wolf");

        }

        public Wolf(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("wolf");
        }
    }
}
