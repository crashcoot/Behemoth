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
    class CollisionObjectsList
    {
        private ObstacleList obstacles;
        private NPCList npcs;

        public CollisionObjectsList(Vector2 pos, int w, int h)
        {
            obstacles = new ObstacleList(pos, w, h);
            npcs = new NPCList();
        }

        public CollisionObject isCollision(Rectangle rect)
        {
            CollisionObject tempCO;
            if ((tempCO = obstacles.isCollision(rect)) != null) {
                return tempCO;
            }
            if ((tempCO = npcs.isCollision(rect)) != null)
            {
                return tempCO;
            }
            return null;
        }

        public void Add(Obstacle ob)
        {
            obstacles.Add(ob);
        }

        public void Add(NPC n)
        {
            npcs.Add(n);
        }

        public List<Obstacle> getAdjacentObstacles(Vector2 pos)
        {
            return obstacles.AdjacentObstacles(pos);
        }

        public void Swing(Vector2 position, int radius, float charged)
        {
            foreach (Obstacle ob in obstacles.AdjacentObstacles(position))
            {
                int sum = radius + ob.Radius;
                if (Vector2.Distance(position, ob.HitPos) < sum)
                {
                    ob.OnHit(position, charged);
                }
            }
            foreach(NPC n in npcs.NPCs)
            {
                int sum = radius + n.Radius;
                if (Vector2.Distance(position, n.HitPos) < sum)
                {
                    n.OnHit(position, charged);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int mapHeight)
        {
            npcs.Draw(spriteBatch, mapHeight);
            obstacles.Draw(spriteBatch, mapHeight);
        }

        public void RemoveDead()
        {
            obstacles.RemoveDead();
        }

        public void Update()
        {
            obstacles.Update(obstacles);
            UpdateMovingObstacles();
            UpdateNPCs();
        }

        public void UpdateNPCs()
        {
            npcs.Update();
            foreach (NPC n1 in npcs.NPCs)
            {
                foreach (Obstacle ob in obstacles.AdjacentObstacles(n1.Position))
                {
                    if (n1.HitBox.Intersects(ob.HitBox))
                    {
                        n1.Health -= 25 * ob.Mass;
                        n1.Momentum *= 1 - ob.Mass;
                        ob.OnHit(n1.Position, n1.Momentum);
                    }
                }
                foreach (NPC n2 in npcs.NPCs)
                {
                    if (n1 != n2 && n1.HitBox.Intersects(n2.HitBox))
                    {
                        n2.OnHit(n1.Position, n1.Momentum);
                    }
                }
            }
        }
        
        public void UpdateMovingObstacles()
        {
            List<Obstacle> movingObstacles = obstacles.MovingObstacles;
            foreach (Obstacle m in movingObstacles)
            {
                m.Update();
                foreach (Obstacle ob in obstacles.AdjacentObstacles(m.Position))
                {
                    if (ob != m && m.HitBox.Intersects(ob.HitBox))
                    {
                        m.Health -= 25 * ob.Mass;
                        m.Momentum *= 1 - ob.Mass;
                        ob.OnHit(m.Position, m.Momentum);
                    }
                }
                foreach (NPC n in npcs.NPCs)
                {
                    if (m.HitBox.Intersects(n.HitBox))
                    {
                        n.OnHit(m.Position, m.Momentum);
                    }
                }
            }
        }
    }
}
