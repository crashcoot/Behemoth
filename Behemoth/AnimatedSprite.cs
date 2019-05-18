using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Behemoth
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double timer;
        private double speed;

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            speed = 0.15D;
            timer = speed;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                currentFrame++;
                timer = speed;
            }

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void setSpeed(double newSpeed)
        {
            speed = newSpeed;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int mapHeight)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(Texture, location, sourceRectangle, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)(location.Y / mapHeight));
            //spriteBatch.End();
        }

        public void setFrame(int newFrame)
        {
            currentFrame = newFrame;
        }
    }
}