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
        private ObstacleList obstacles;
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
            moving = false;
            if (health <= 0)
            {
                dead = true;
                return;
            }
            if (momentum > 0)
            {
                moving = true;
                
                position.X += launchDirection.X * momentum * 0.35F;
                position.Y += launchDirection.Y * momentum * 0.35F;
                hitPos.X = position.X + 16;
                hitPos.Y = position.Y + 16;
                hitBox.X = (int)position.X + 5;
                hitBox.Y = (int)position.Y + 5;
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
