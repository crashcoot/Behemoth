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
        private int radius =  32;
        private bool finished = false;
        private float startTime;
        private float lifeTime = 0.2f;
        private Dir direction;
        private int displacement = 24;

        public Swing(Vector2 newPos, GameTime time, Dir newDir)
        {
            position = newPos;
            startTime = (float)time.ElapsedGameTime.TotalSeconds;
            direction = newDir;

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

        public float LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }

        public Dir Direction
        {
            get { return direction; }
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            switch (direction)
            {
                case Dir.Up:
                    position.X = playerPos.X;
                    position.Y = playerPos.Y - displacement;
                    break;
                case Dir.LeftUp:
                    position.X = playerPos.X - displacement;
                    position.Y = playerPos.Y - displacement;
                    break;
                case Dir.RightUp:
                    position.X = playerPos.X + displacement;
                    position.Y = playerPos.Y - displacement;
                    break;
                case Dir.Down:
                    position.X = playerPos.X;
                    position.Y = playerPos.Y + displacement;
                    break;
                case Dir.LeftDown:
                    position.X = playerPos.X - displacement;
                    position.Y = playerPos.Y + displacement;
                    break;
                case Dir.RightDown:
                    position.X = playerPos.X + displacement;
                    position.Y = playerPos.Y + displacement;
                    break;
                case Dir.Left:
                    position.X = playerPos.X - displacement;
                    position.Y = playerPos.Y;
                    break;
                case Dir.Right:
                    position.X = playerPos.X + displacement;
                    position.Y = playerPos.Y;
                    break;
                default:
                    break;
            }
        }
    }
}
