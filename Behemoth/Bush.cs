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
    class Bush : Obstacle
    {
        public Bush(Vector2 newPos, Texture2D tex) : base(newPos, tex)
        {
            radius = 12;
            hitPos = new Vector2(position.X + 32, position.Y);
            drawSort = 32;
            hitBox = new Rectangle((int)position.X + 16, (int)position.Y + 12, 30, 15);
            mass = 0.1F;
        }

        public override void OnHit(Vector2 otherPos, float power)
        {
            dead = true;
        }

        public override void Update()
        {

        }
    }
}
