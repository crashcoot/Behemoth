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
    class Player
    {
        private Vector2 position = new Vector2(500,500);
        private Vector2 positionOld;
        private Dir direction = Dir.Down;
        private Dir directionOld;
        private bool isMoving = false;
        private int defaultSpeed = 100;
        private float maxSpeed = 400;
        private float speed;
        private int radius = 16;
        private Rectangle hitBox;
        private KeyboardState kStateOld = Keyboard.GetState();
        private int swingDisplacement = 16;
        private float stamina = 100;
        private float maxStamina = 100;
        private Swing swing = null;

        public AnimatedSprite anim;
        public AnimatedSprite[] animations = new AnimatedSprite[8];

        const int topBuffer = 64;

        public Player()
        {
            speed = defaultSpeed;
            hitBox = new Rectangle((int)position.X - 13, (int)position.Y - 13, 26, 26);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void SetX(float newX)
        {
            position.X = newX;
        }

        public void SetY(float newY)
        {
            position.Y = newY;
        }

        public float Stamina
        {
            get { return stamina; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public Swing Swing
        {
            get { return swing; }
        }

        public void Update(GameTime gameTime, int mapW, int mapH)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            hitBox.X = (int)position.X - 13;
            hitBox.Y = (int)position.Y;

            anim = animations[(int)direction];

            if (isMoving)
            {
                anim.Update(gameTime);
            }
            else
            {
                anim.setFrame(0);
            }

            isMoving = false;

            if (kState.IsKeyDown(Keys.Right) && !(kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.Down)))
            {
                direction = Dir.Right;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Left) && !(kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.Down)))
            {
                direction = Dir.Left;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Up) && !(kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.Right)))
            {
                direction = Dir.Up;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Down) && !(kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.Right)))
            {
                direction = Dir.Down;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Left))
            {
                direction = Dir.LeftUp;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Right))
            {
                direction = Dir.RightUp;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Right))
            {
                direction = Dir.RightDown;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Left))
            {
                direction = Dir.LeftDown;
                isMoving = true;
            }

            if (isMoving)
            {
                Obstacle tempOb = null;
                Rectangle tempRect = hitBox;
                UTurnCheck();
                tempRect.X += (int)Math.Ceiling((speed * dt * Math.Cos((int)direction * Math.PI / 4)));
                if ((tempOb = Obstacle.didCollide(tempRect)) == null && tempRect.X < mapW && tempRect.X > 0)
                {
                    position.X += (float)(speed * dt * Math.Cos((int)direction * Math.PI / 4));
                }
                else
                {
                    Collision(tempOb);
                }
                tempRect = hitBox;
                tempRect.Y -= (int)Math.Ceiling((speed * dt * Math.Sin((int)direction * Math.PI / 4)));
                if ((tempOb = Obstacle.didCollide(tempRect)) == null && tempRect.X < mapW && tempRect.Y > 64)
                {
                    position.Y -= (float)(speed * dt * Math.Sin((int)direction * Math.PI / 4));
                }
                else
                {
                    Collision(tempOb);
                }
            }
            //Player is not moving
            else
            {
                if (stamina < maxStamina)
                {
                    stamina += dt * 5;
                }
                
            }
            if (kState.IsKeyDown(Keys.LeftShift) && stamina > 0)
            {
                //If the player is actually moving
                if (isMoving && position.X != positionOld.X || position.Y != positionOld.Y)
                {

                    if (speed < maxSpeed)
                    {
                        speed *= 1.01F * dt / 0.0166667F;
                    }
                    if (stamina - dt < 0)
                    {
                        stamina = 0;
                    }
                    else
                    {
                        stamina -= dt;
                    }
                }
                else
                {
                    speed = defaultSpeed;
                }
            }
            //More stamina for not holding down sprint
            else if (stamina < maxStamina)
            {
                stamina += dt * 5;
                speed = defaultSpeed;
            }

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space) && stamina >= 10)
            {
                //MySounds.projectileSound.Play(0.2f, 0.5f, 0f);
                stamina -= 10;
                swing = new Swing(position, gameTime, direction);
            }
            
            if (swing != null)
            {
                swing.LifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (swing.LifeTime < 0)
            {
                swing = null;
            }
            }
            
            anim.setSpeed(0.25D - (0.2D * speed/maxSpeed));
            kStateOld = kState;
            directionOld = direction;
            positionOld = position;
        }

        private void Collision(Obstacle ob)
        {
            if (ob != null && speed > defaultSpeed*3 )
            {
                speed = defaultSpeed;
                if (stamina > 5)
                {
                    stamina -= 5;
                    ob.OnHit(position, 5);
                }
                
            }
        }

        private void UTurnCheck()
        {
            if (Math.Abs((int)direction - (int)directionOld) == 4)
            {
                speed = defaultSpeed;
            }
        }
    }
}
