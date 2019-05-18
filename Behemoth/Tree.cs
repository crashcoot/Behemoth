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
    class Tree : Obstacle
    {
        
        public Tree(Vector2 newPos, Texture2D tex) : base(newPos, tex)
        {
            radius = 14;
            hitPos = new Vector2(position.X + 32, position.Y + 85);
            hitBox = new Rectangle((int)position.X + 20, (int)position.Y + 80, 20, 20);
            drawSort = 40;
        }
    }
}
