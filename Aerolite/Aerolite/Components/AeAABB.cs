using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public class AeAABB : AeComponent
    {
        Rectangle _rect = new Rectangle();

        public void SetSize(int width, int height)
        {
            _rect.Width = width;
            _rect.Height = height;
        }

        public void SetPosition(int x, int y)
        {
            _rect.X = x;
            _rect.Y = y;
        }

        public bool Overlaps(AeAABB otherHull)
        {
            if (this._rect.Intersects(otherHull._rect))
            {
                return true;
            }
            return false;
        }
    }
}
