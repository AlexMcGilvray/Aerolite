using Aerolite.Entity;
using Microsoft.Xna.Framework;

namespace Aerolite.Components
{
    public class AeCamera : AeComponent
    {
        public AeTransform Transform { get; private set; }

        public AeEntity ApproachTarget { get; private set; }
        public float ApproachSpeed { get; set; } = 15.0f;
        public float ApproachThreshold { get; set; } = 16.0f;
        private bool _removeTargetOnArrival;

        public void SetApproachTarget(AeEntity target, bool removeTargetOnArrival = false)
        {
            ApproachTarget = target;
            _removeTargetOnArrival = removeTargetOnArrival;
        }

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
            convertedCoordinates.X = (screenCoordinate.X / Transform.ScaleX) - Transform.X;
            convertedCoordinates.Y = (screenCoordinate.Y / Transform.ScaleY) + Transform.Y;
            return convertedCoordinates; 
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateScale(Transform.ScaleX,Transform.ScaleY,1.0f) * 
                Matrix.CreateTranslation(Transform.X * Transform.ScaleX, -Transform.Y * Transform.ScaleY, 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ApproachTarget != null)
            {
                Vector2 vectorToTarget;
                Vector2 directionToTarget;
                vectorToTarget.X = ApproachTarget.Transform.X - Transform.X;
                vectorToTarget.Y = ApproachTarget.Transform.Y - Transform.Y;
                directionToTarget = vectorToTarget;
                if (directionToTarget.LengthSquared() != 0)
                {
                    directionToTarget.Normalize();
                }
                Transform.X += directionToTarget.X * ApproachSpeed;
                Transform.Y += directionToTarget.Y * ApproachSpeed;
                if (vectorToTarget.LengthSquared() <= ApproachThreshold * ApproachThreshold)
                {
                    if (_removeTargetOnArrival)
                    {
                        ApproachTarget = null;
                    }
                }
            }
        }
    }
}