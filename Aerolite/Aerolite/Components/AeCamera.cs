using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public class AeCamera : AeComponent
    {
        public AeTransform Transform { get; private set; }

        public AeCamera()
        {
            Transform = new AeTransform();
        }
        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(Transform.X, Transform.Y, 0.0f);
        }
    }
}
