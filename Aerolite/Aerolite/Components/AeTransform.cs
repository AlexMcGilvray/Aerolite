using Aerolite.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public class AeTransform : AeComponent
    {
        private float positionX, positionY;
        private float orientationX, orientationY;
        private float scaleX, scaleY;

        public Vector2 Position
        {
            get
            {
                return new Vector2(positionX, positionY);
            }
            set
            {
                positionX = value.X;
                positionY = value.Y;
            }
        }

        public float X { get { return positionX; } set { positionX = value; } }
        public float Y { get { return positionY; } set { positionY = value; } }

        public float OrientatioX { get { return orientationX; } set { orientationX = value; } }
        public float OrientatioY { get { return orientationY; } set { orientationY = value; } }

        public float ScaleX { get { return scaleX; } set { scaleX = value; } }
        public float ScaleY { get { return scaleY; } set { scaleY = value; } }
    }
}
