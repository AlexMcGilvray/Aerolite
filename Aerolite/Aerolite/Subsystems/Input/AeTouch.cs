using Aerolite.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeTouch 
    {
        TouchCollection _touchCollection;

        public void Poll()
        {
            _touchCollection = TouchPanel.GetState();
        }

        public bool IsTouched(AeAABB boundingBox)
        {
            foreach (var touch in _touchCollection)
            {
                if (boundingBox.Overlaps(new Vector2(touch.Position.X, touch.Position.Y)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AnyTouched()
        {
            return _touchCollection.Count > 0;
        }
    }
}
