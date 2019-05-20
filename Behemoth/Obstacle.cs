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
        protected float health;
        public abstract void Update(ObstacleList obstacles);
        protected float mass;
        protected bool moving = false;

        public Obstacle(Vector2 newPos, Texture2D newTex)
        {
            position = newPos;
            texture = newTex;
        }

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

        public float Mass
        {
            get { return mass; }
        }

        public bool Moving
        {
            get { return moving; }
        }
        
    }
}
