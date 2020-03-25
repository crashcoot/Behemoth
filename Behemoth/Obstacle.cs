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
    abstract class Obstacle : CollisionObject
    {
        protected Vector2 position;
        protected int radius;
        protected Vector2 hitPos;
        protected bool dead = false;
        private Texture2D texture;
        protected int drawSort;
        protected float momentum = 0;
        protected Vector2 launchDirection;
        protected float health;
        public abstract void Update();
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

        public void Draw(SpriteBatch spriteBatch, int mapHeight)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((hitPos.Y - drawSort) / mapHeight));
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

        public float Momentum
        {
            get { return momentum; }
            set { momentum = value; }
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }
        
    }
}
