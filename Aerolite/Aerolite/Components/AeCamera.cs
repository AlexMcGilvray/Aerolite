using Aerolite.Entity;
using Microsoft.Xna.Framework;

namespace Aerolite.Components
{
    public enum AeCameraApproachTargetMode
    {
        Normal,
        ApproachYOnly,
        ApproachXOnly
    }

    public class AeCameraApproachTarget
    {
        public AeCameraApproachTarget(AeEntity target, AeCameraApproachTargetMode mode = AeCameraApproachTargetMode.Normal)
        {
            Target = target;
            ApproachMode = mode;
        }
        public AeEntity Target { get; private set; }
        public AeCameraApproachTargetMode ApproachMode { get; private set; }
    }

    public class AeCamera : AeComponent
    {
        public AeTransform Transform { get; private set; }
        public AeEntity ApproachTarget => _cameraApproachTarget?.Target;
        
        public float ApproachSpeed { get; set; } = 15.0f;
        public float ApproachThreshold { get; set; } = 16.0f;

        public void SetApproachTarget(AeEntity target, bool removeTargetOnArrival = false) => SetApproachTarget(target, AeCameraApproachTargetMode.Normal, removeTargetOnArrival);

        public void SetApproachTarget(AeEntity target, AeCameraApproachTargetMode mode, bool removeTargetOnArrival = false)
        {
            _cameraApproachTarget = new AeCameraApproachTarget(target, mode);
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
            if (_cameraApproachTarget != null)
            {
                Vector2 vectorToTarget = Vector2.Zero;
                Vector2 directionToTarget;
                switch (_cameraApproachTarget.ApproachMode)
                {
                    case AeCameraApproachTargetMode.Normal:
                        vectorToTarget.X = _cameraApproachTarget.Target.Transform.X - Transform.X;
                        vectorToTarget.Y = _cameraApproachTarget.Target.Transform.Y - Transform.Y;
                        break;
                    case AeCameraApproachTargetMode.ApproachYOnly:
                        vectorToTarget.Y = _cameraApproachTarget.Target.Transform.Y - Transform.Y;
                        break;
                    case AeCameraApproachTargetMode.ApproachXOnly:
                        vectorToTarget.X = _cameraApproachTarget.Target.Transform.X - Transform.X;
                        break;
                    default:
                        break;
                }
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
                        _cameraApproachTarget = null;
                    }
                }
            }
        }

        private bool _removeTargetOnArrival;
        private AeCameraApproachTarget _cameraApproachTarget;
    }
}