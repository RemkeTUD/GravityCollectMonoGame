using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class ItemConnection
    {
        public Vector2 offset;
        public Item item;

        public ItemConnection(Vector2 offset, Item item)
        {
            this.offset = offset;
            this.item = item;
        }

        public void update(Vector2 pos)
        {
            item.pos = pos + offset;
            item.Update();
        }
        public void draw(SpriteBatch batch, Vector2 pos)
        {
            item.pos = pos + offset;
            item.Draw(batch);
        }

    }
}
