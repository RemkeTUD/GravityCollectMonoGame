using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Teleporter : Item
    {
        public TeleporterDestination dest;
        public Teleporter(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("teleporter");
        }

        public override void Update()
        {
            if (CollidesWithPlayer())
                Game1.getPlayer().pos = dest.pos;
            base.Update();
        }

    }
}
