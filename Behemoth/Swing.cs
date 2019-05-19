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
    class Swing
    {
        private Vector2 position;
        private int radius = 24;
        private bool finished = false;
        private float startTime;
        private bool active = true;
        private Dir direction;
        private int displacement = 24;
        private float charged;

        public Swing(Vector2 newPos, GameTime time, Dir newDir, float ch)
        {
            position = newPos;
            startTime = (float)time.ElapsedGameTime.TotalSeconds;
            direction = newDir;
            charged = ch;

            switch (direction)
            {
                case Dir.Up:
                    position.X = newPos.X;
                    position.Y = newPos.Y - displacement;
                    break;
                case Dir.LeftUp:
                    position.X = newPos.X - displacement * 0.7f;
                    position.Y = newPos.Y - displacement * 0.7f;
                    break;
                case Dir.RightUp:
                    position.X = newPos.X + displacement * 0.7f;
                    position.Y = newPos.Y - displacement * 0.7f;
                    break;
                case Dir.Down:
                    position.X = newPos.X;
                    position.Y = newPos.Y + displacement;
                    break;
                case Dir.LeftDown:
                    position.X = newPos.X - displacement * 0.7f;
                    position.Y = newPos.Y + displacement * 0.7f;
                    break;
                case Dir.RightDown:
                    position.X = newPos.X + displacement * 0.7f;
                    position.Y = newPos.Y + displacement * 0.7f;
                    break;
                case Dir.Left:
                    position.X = newPos.X - displacement;
                    position.Y = newPos.Y;
                    break;
                case Dir.Right:
                    position.X = newPos.X + displacement;
                    position.Y = newPos.Y;
                    break;
                default:
                    break;
            }
        }

        public bool Finished
        {
            get { return finished; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public bool Active
        {
            get { return active; }
        }

        public Dir Direction
        {
            get { return direction; }
        }

        public float Charged
        {
            get { return charged; }
        }

        public void Update(GameTime gameTime, Player player)
        {
            active = false;
            foreach (Obstacle ob in Obstacle.obstacles)
            {
                int sum = player.Swing.Radius + ob.Radius;
                if (Vector2.Distance(player.Swing.Position, ob.HitPos) < sum)
                {
                    ob.OnHit(player.Position, player.Swing.Charged);
                }
            }
        }
    }
}
