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
    class ObstacleList
    {
        private Vector2 position;
        int width;
        int height;
        const int sectionSize = 100;
        int rows;
        int columns;
        private List<Obstacle>[,] obstacles;
        private List<Obstacle> movingObstacles;

        public ObstacleList(Vector2 pos, int w, int h)
        {
            position = pos;
            width = w + 1000;
            height = h + 1000;
            rows = width / sectionSize;
            columns = height / sectionSize;
            obstacles = new List<Obstacle>[columns,rows];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    obstacles[x, y] = new List<Obstacle>();
                }
            }
            movingObstacles = new List<Obstacle>();
        }

        public void Add(Obstacle ob)
        {
            int x = (int)ob.Position.X / sectionSize;
            int y = (int)ob.Position.Y / sectionSize;
            obstacles[x, y].Add(ob);
        }

        public void Draw(SpriteBatch spriteBatch, int mapHeight)
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                    foreach (Obstacle ob in obstacles[x, y])
                    {
                        ob.Draw(spriteBatch, mapHeight);
                    }
            }
            foreach(Obstacle ob in movingObstacles)
            {
                ob.Draw(spriteBatch, mapHeight);
            }

        }

        public void Update(ObstacleList obs)
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    foreach(Obstacle ob1 in obstacles[x, y])
                    {
                        ob1.Update();
                    }
                    foreach (Obstacle ob2 in obstacles[x, y])
                    {
                        if (ob2.Moving)
                        {
                            movingObstacles.Add(ob2);
                        }
                    }
                    obstacles[x, y].RemoveAll(ob => ob.Dead);
                    obstacles[x, y].RemoveAll(ob => ob.Moving);
                }
            }
            foreach (Obstacle ob in movingObstacles)
            {
                if (!ob.Moving)
                {
                    int x = (int)ob.Position.X / sectionSize;
                    int y = (int)ob.Position.Y / sectionSize;
                    obstacles[x,y].Add(ob);
                }
            }
            movingObstacles.RemoveAll(ob => !ob.Moving);

            
            movingObstacles.RemoveAll(ob => !ob.Moving);
        }

        public List<Obstacle> MovingObstacles
        {
            get { return movingObstacles; }
        }

        public void RemoveDead()
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    obstacles[x,y].RemoveAll(ob => ob.Dead);
                }
            }
        }

        public List<Obstacle> AdjacentObstacles(Vector2 position)
        {
            List<Obstacle> tempList = new List<Obstacle>();
            int x = (int)position.X / sectionSize;
            if (x < 0) x = 0;
            int y = (int)position.Y / sectionSize;
            if (y < 0) y = 0;
            List<List<Obstacle>> sections = new List<List<Obstacle>>();
            if (x != 0 && y != 0) { sections.Add(obstacles[x - 1, y - 1]); }
            if (y != 0) { sections.Add(obstacles[x, y - 1]); }
            if (x != columns-1 && y != 0) { sections.Add(obstacles[x + 1, y - 1]); }
            if (x != 0) { sections.Add(obstacles[x - 1, y]); }
            sections.Add(obstacles[x, y]);
            if (x != columns-1) { sections.Add(obstacles[x + 1 , y]); }
            if (x != 0 && y != rows-1) { sections.Add(obstacles[x - 1, y + 1]); }
            if (y != rows-1) { sections.Add(obstacles[x, y + 1]); }
            if (x != rows-1 && y != columns-1) { sections.Add(obstacles[x + 1, y + 1]); }
            foreach(List<Obstacle> list in sections)
            {
                foreach(Obstacle ob in list)
                {
                    tempList.Add(ob);
                }
            }
            return tempList.Concat(movingObstacles).ToList();
        }

        public Obstacle isCollision(Rectangle otherRect)
        {
            List<Obstacle> tempList = AdjacentObstacles(new Vector2(otherRect.X, otherRect.Y));
            foreach(Obstacle ob in tempList)
            {
                if (ob.HitBox.Intersects(otherRect))
                {
                    return ob;
                }
            }
            foreach(Obstacle ob in movingObstacles)
            {
                if (ob.HitBox.Intersects(otherRect))
                {
                    return ob;
                }
            }
            return null;
        }
    }
}
