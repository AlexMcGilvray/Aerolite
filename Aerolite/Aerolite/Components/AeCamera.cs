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
            Transform.ScaleX = 1.0f;
            Transform.ScaleY = 1.0f;
        }

        /// <summary>
        /// NOTE doesnt take into account scale yet
        /// </summary>
        /// <returns></returns>
        public AeAABB GetBoundingBox()
        {
            AeAABB boundingBox = new AeAABB();

            boundingBox.SetPosition((int)Transform.X, (int)Transform.Y);
            boundingBox.SetSize(Engine.Graphics.GraphicsSettings.GameResolutionWidth, Engine.Graphics.GraphicsSettings.GameResolutionHeight);

            return boundingBox;
        }

        public Vector2 ScreenToWorld(Vector2 screenCoordinate)
        {
            Vector2 convertedCoordinates;
            convertedCoordinates.X = screenCoordinate.X - Transform.X;
            convertedCoordinates.Y = screenCoordinate.Y - Transform.Y;
            return convertedCoordinates;
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateScale(Transform.ScaleX,Transform.ScaleY,1.0f) * 
                Matrix.CreateTranslation(Transform.X, Transform.Y, 0.0f);
        }
    }
}