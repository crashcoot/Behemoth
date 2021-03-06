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
    class Tree : Obstacle
    {
        
        public Tree(Vector2 newPos, Texture2D tex) : base(newPos, tex)
        {
            radius = 14;
            hitPos = new Vector2(position.X + 32, position.Y + 100);
            hitBox = new Rectangle((int)position.X + 20, (int)position.Y + 80, 20, 20);
            drawSort = 50;
            mass = 0.5F;
            health = 20;
        }

        public override void OnHit(Vector2 otherPos, float power)
        {
            health -= power;
        }

        public override void Update()
        {
            if (health <= 0)
            {
                dead = true;
            }
        }
    }
}
