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
    class NPCList
    {
        private List<NPC> Npcs;

        public NPCList()
        {
            Npcs = new List<NPC>();
        }

        public CollisionObject isCollision(Rectangle rect)
        {
            foreach (NPC npc in Npcs)
            {
                if (rect.Intersects(npc.HitBox))
                {
                    return npc;
                }
            }
            return null;
        }

        public void Update()
        {
            foreach (NPC n in Npcs)
            {
                n.Update();
            }
            NPCs.RemoveAll(npc => npc.Dead);
        }

        public void Add(NPC n)
        {
            NPCs.Add(n);
        }

        public List<NPC> NPCs
        {
            get { return Npcs; }
        }

        public void Draw(SpriteBatch spriteBatch, int mapHeight)
        {
            foreach (NPC n in Npcs)
            {
                n.Draw(spriteBatch, mapHeight);
            }
        }
    }

    
}
