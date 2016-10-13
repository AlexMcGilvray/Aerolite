using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Entity
{
    public class AeTransform
    {
        private float positionX, positionY, positionZ;
        private float orientationX, orientationY, orientationZ;
        private float scaleX, scaleY, scaleZ;

        public float X { get { return positionX; } set { positionX = value; } }
        public float Y { get { return positionY; } set { positionY = value; } }
        public float Z { get { return positionZ; } set { positionZ = value; } }
    }
}
