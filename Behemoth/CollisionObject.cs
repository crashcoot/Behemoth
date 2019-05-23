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
    abstract class CollisionObject
    {
        protected Rectangle hitBox;
        public abstract void OnHit(Vector2 otherPos, float power);
    }
}
