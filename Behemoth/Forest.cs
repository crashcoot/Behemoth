using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Behemoth
{
    class Forest
    {
        private float timer;
        private float defaultTimer = 5;
        private Vector2 topLeft;
        private int width;
        private int height;
        private Texture2D treeSprite;
        private Texture2D bushSprite;
        private Texture2D boulderSprite;
        private double timeSinceLastTree = 0;

        public Forest(Vector2 tLeft, int w, int h, int columns, int rows, Texture2D tree, Texture2D bush, Texture2D boulder)
        {
            timer = defaultTimer;
            treeSprite = tree;
            bushSprite = bush;
            boulderSprite = boulder;
            topLeft = tLeft;
            width = w;
            height = h;

            var rnd = new Random(DateTime.Now.Millisecond);
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Obstacle.obstacles.Add(new Tree(new Vector2(rnd.Next(x * width / columns + 16, (x + 1) * width / columns) - 16, rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), treeSprite));
                }
            }
            for (int x = 0; x < columns; x += 2)
            {
                for (int y = 0; y < rows; y += 2)
                {
                    Bush tempBush = new Bush(new Vector2(rnd.Next(x * width / columns + 16, (x + 2) * width / columns), rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), bushSprite);
                    if (Obstacle.didCollide(tempBush.HitBox) == null)
                    {
                        Obstacle.obstacles.Add(tempBush);
                    }
                }
            }

            for (int x = 0; x < columns; x += 3)
            {
                for (int y = 0; y < rows; y += 3)
                {
                    Boulder tempBoulder = new Boulder(new Vector2(rnd.Next(x * width / columns + 16, (x + 2) * width / columns), rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), boulderSprite);
                    if (Obstacle.didCollide(tempBoulder.HitBox) == null)
                    {
                        Obstacle.obstacles.Add(tempBoulder);
                    }
                }
            }
        }

        public void Update(GameTime gameTime, Player player, float camW, float camH)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Obstacle tempOb;
            int tries = 0;
            int treesClose = 0;
            if (timer <= 0)
            {
                Tree tempTree = new Tree(new Vector2(rnd.Next((int)topLeft.X, width), rnd.Next((int)topLeft.Y, height)), treeSprite);
                while(true)
                {
                    tempTree = new Tree(new Vector2(rnd.Next((int)topLeft.X, width), rnd.Next((int)topLeft.Y, height)), treeSprite);
                    if ((tempOb = Obstacle.didCollide(tempTree.HitBox)) == null && IsOffScreen(tempTree.Position, player, camW, camH))
                    {
                        foreach (Obstacle ob in Obstacle.obstacles)
                        {
                            if (Vector2.Distance(ob.Position, tempTree.Position) < 200)
                            {
                                treesClose++;
                            }
                        }
                        if (treesClose >= rnd.Next(1, 4))
                        {
                            Obstacle.obstacles.Add(tempTree);
                            Console.WriteLine("Time: " + (gameTime.TotalGameTime.TotalSeconds - timeSinceLastTree));
                            timeSinceLastTree = gameTime.TotalGameTime.TotalSeconds;
                        }
                        break;
                    }
                    if (tries > 5) { break; }
                    tries++;
                }
                timer = defaultTimer;
            }
        }

        //Returns true if Vector2 obPos is currently out of visible range
        private bool IsOffScreen(Vector2 obPos, Player player, float camW, float camH)
        {
            float minX = player.Position.X - camW / 2;
            float maxX = player.Position.X + camW / 2;
            float minY = player.Position.Y - camH / 2;
            float maxY = player.Position.Y + camH / 2;

            return (obPos.X <= minX || obPos.X >= maxX || obPos.Y <= minY || obPos.Y >= maxY);
        }
    }
}
