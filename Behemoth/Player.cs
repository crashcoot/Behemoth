﻿using System;
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
        private float strength = 100;
        private float maxStrength = 100;
        private Swing swing = null;
        private float charge = 0;
        private State state;

        public AnimatedSprite anim;
        //0 = Walking  1 = charging  2 = swing
        public AnimatedSprite[][] animations = { new AnimatedSprite[8], new AnimatedSprite[8], new AnimatedSprite[8] };
        private enum State
        {
               Walking,
               Charging,
               Swing
        }

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

        public float Strength
        {
            get { return strength; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public Swing Swing
        {
            get { return swing; }
        }

        public Dir Direction
        {
            get { return direction; }
        }

        public void Update(CollisionObjectsList obstacles, GameTime gameTime, int mapW, int mapH)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            state = State.Walking;

            hitBox.X = (int)position.X - 13;
            hitBox.Y = (int)position.Y;

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
                CollisionObject tempOb = null;
                Rectangle tempRect = hitBox;
                UTurnCheck();
                tempRect.X += (int)Math.Ceiling((speed * dt * Math.Cos((int)direction * Math.PI / 4)));
                if ((tempOb = obstacles.isCollision(tempRect)) == null && tempRect.X < mapW && tempRect.X > 0)
                {
                    position.X += (float)(speed * dt * Math.Cos((int)direction * Math.PI / 4));
                }
                else
                {
                    Collision(tempOb);
                }
                tempRect = hitBox;
                tempRect.Y -= (int)Math.Ceiling((speed * dt * Math.Sin((int)direction * Math.PI / 4)));
                if ((tempOb = obstacles.isCollision(tempRect)) == null && tempRect.Y < mapH && tempRect.Y > 64)
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
            else
            {
                speed = defaultSpeed;
            }
            //Spacebar just pressed
            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                if (strength >= 1)
                {
                    strength -= 1;
                    charge += 0.4f;
                }
            }
            //Spacebar being held
            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyDown(Keys.Space))
            {
                state = State.Charging;
                if (strength >= 1)
                {
                    strength -= 1;
                    charge += 0.4f;
                }
            }
            //Spacebar released
            if (kState.IsKeyUp(Keys.Space) && kStateOld.IsKeyDown(Keys.Space))
            {
                //MySounds.projectileSound.Play(0.2f, 0.5f, 0f);
                state = State.Swing;
                swing = new Swing(position, gameTime, direction, charge);
                charge = 0;
            }
            if (swing != null && !swing.Active)
            {
                 swing = null;
            }

            if (stamina > 0 && strength < maxStrength && kState.IsKeyUp(Keys.Space))
            {
                stamina -= dt * 0.4f;
                strength += dt * 40f;
            }
            
            
            anim = animations[(int)state][(int)direction];
            anim.setSpeed(0.25D - (0.2D * speed / maxSpeed));

            if (isMoving)
            {
                anim.Update(gameTime);
            }
            else
            {
                anim.setFrame(0);
            }
            kStateOld = kState;
            directionOld = direction;
            positionOld = position;
        }

        private void Collision(CollisionObject ob)
        {
            if (ob != null && speed > defaultSpeed*3 )
            {
                if (stamina >= 2)
                {
                    stamina -= 2;
                    ob.OnHit(position, speed/maxSpeed*30);
                }
                speed = defaultSpeed;
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
