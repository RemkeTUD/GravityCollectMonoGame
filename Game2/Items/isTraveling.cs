using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public interface isTraveling
    {
        void SetEndPoint(Vector2 endpoint);
        Vector2 getEndPoint();
    }
}
