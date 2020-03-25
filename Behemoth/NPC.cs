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
    class NPC : CollisionObject
    {
        private Vector2 position;
        private float health = 100;
        private float momentum;
        private Vector2 launchDirection;
        private int drawSort = 32;
        private Texture2D texture;
        private Vector2 hitPos;
        private bool dead = false;
        private int radius = 16;

        public NPC(Vector2 newPos, Texture2D tex)
        {
            position = newPos;
            hitBox = new Rectangle((int)position.X + 3, (int)position.Y + 3, 26, 26);
            hitPos = new Vector2(position.X, position.Y);
            texture = tex;
        }

        public void Update()
        {
            if (momentum > 0)
            {

                position.X += launchDirection.X * momentum * 0.35F;
                position.Y += launchDirection.Y * momentum * 0.35F;
                hitPos.X = position.X + 16;
                hitPos.Y = position.Y + 16;
                hitBox.X = (int)position.X + 5;
                hitBox.Y = (int)position.Y + 5;
                if (momentum < 30)
                {
                    momentum *= 0.8F;
                }
                else
                {
                    momentum *= 0.99F;
                }
            }
            if (health < 0)
            {
                dead = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int mapHeight)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((hitPos.Y - drawSort) / mapHeight));
        }
    

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public int DrawSort
        {
            get { return drawSort; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Vector2 HitPos
        {
            get { return hitPos; }
        }

        public bool Dead
        {
            get { return dead; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public float Momentum
        {
            get { return momentum; }
            set { momentum = value; }
        }

        public override void OnHit(Vector2 otherPos, float power)
        {
            launchDirection = Vector2.Subtract(position, otherPos);
            launchDirection.Normalize();
            momentum = power;
            health -= power;
        }

        
    }
}
