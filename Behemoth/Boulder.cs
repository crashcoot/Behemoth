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
            mass = 0.8F;
            health = 140;
        }

        public override void OnHit(Vector2 otherPos, float power)
        {
                launchDirection = Vector2.Subtract(position, otherPos);
                launchDirection.Normalize();
                momentum = power;
                health -= power;
        }

        public override void Update()
        {
            if (health <= 0)
            {
                dead = true;
                return;
            }
            if (momentum > 0)
            {
                foreach (Obstacle ob in Obstacle.obstacles)
                {
                    if (ob != this && hitBox.Intersects(ob.HitBox))
                    {
                        health -= 25 * ob.Mass;
                        momentum *= 1 - ob.Mass;
                        ob.OnHit(position, momentum);
                    }
                }
                position.X += launchDirection.X * momentum * 0.35F;
                position.Y += launchDirection.Y * momentum * 0.35F;
                hitPos = new Vector2(position.X + 16, position.Y + 16);
                hitBox = new Rectangle((int)position.X + 5, (int)position.Y + 10, 25, 20);
                if (momentum < 30)
                {
                    momentum *= 0.8F;
                } else
                {
                    momentum *= 0.99F;
                }
            }
        }
    }
}
