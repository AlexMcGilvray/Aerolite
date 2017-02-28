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
        private float orientation;
        private float scaleX, scaleY;

        public AeTransform()
        {
            scaleX = 1.0f;
            scaleY = 1.0f;
        }

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

        public float Orientation { get { return orientation; } set { orientation = value; } }

        public float ScaleX { get { return scaleX; } set { scaleX = value; } }
        public float ScaleY { get { return scaleY; } set { scaleY = value; } }
    }
}
