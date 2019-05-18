using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Behemoth
{
    class Boulder : Obstacle
    {
        public Boulder(Vector2 newPos, Texture2D tex) : base(newPos, tex)
        {
            radius = 15;
            hitPos = new Vector2(position.X + 16, position.Y + 16);
            hitBox = new Rectangle((int)position.X + 5, (int)position.Y + 10, 25, 20);
            drawSort = 32;
        }
    }
}
