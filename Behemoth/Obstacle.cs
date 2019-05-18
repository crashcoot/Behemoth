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
    abstract class Obstacle
    {
        protected Vector2 position;
        protected int radius;
        protected Vector2 hitPos;
        protected Rectangle hitBox;
        protected bool dead = false;
        private Texture2D texture;
        protected int drawSort;
        public abstract void OnHit(Vector2 otherPos, float power);
        protected float momentum = 0;
        protected Vector2 launchDirection;
        public abstract void Update();

        public static List<Obstacle> obstacles = new List<Obstacle>();

        public Vector2 HitPos
        {
            get { return hitPos; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public int DrawSort
        {
            get { return drawSort; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public Obstacle(Vector2 newPos, Texture2D newTex)
        {
            position = newPos;
            texture = newTex;
        }

        public static Obstacle didCollide(Rectangle otherRect)
        {
            foreach (Obstacle o in Obstacle.obstacles)
            {
                if (o.hitBox.Intersects(otherRect))
                {
                    return o;
                }
            }
            return null;
        }
    }
}
