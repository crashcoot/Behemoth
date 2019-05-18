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

        public override void OnHit(Vector2 otherPos, float power)
        {
                launchDirection = Vector2.Subtract(position, otherPos);
                launchDirection.Normalize();
                momentum = power;
        }

        public override void Update()
        {
            if (momentum > 0)
            {
                foreach (Obstacle ob in Obstacle.obstacles)
                {
                    if (ob != this && hitBox.Intersects(ob.HitBox))
                    {
                        ob.OnHit(position, momentum);
                        momentum *= 0.3F;
                    }
                }
                position.X += launchDirection.X * momentum;
                position.Y += launchDirection.Y * momentum;
                hitPos = new Vector2(position.X + 16, position.Y + 16);
                hitBox = new Rectangle((int)position.X + 5, (int)position.Y + 10, 25, 20);
                momentum *= 0.9F;
            }
        }
    }
}
