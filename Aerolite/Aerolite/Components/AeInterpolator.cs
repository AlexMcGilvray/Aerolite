using Microsoft.Xna.Framework;
using System;

namespace Aerolite.Components
{
    public delegate void AeEventOnInterpolationComplete(AeInterpolator interpolator);

    public enum AeInterpolationType
    {
        OneShot,
        ContinuousSinWave
    }

    public enum AeInterpolationDefaultValueType
    {
        Linear,
        Quadratic
    }

    public class AeInterpolator : AeComponent
    {
        public float CurrentLinearValue { get { return _currentTime / (float)CurrentTarget; } }
        public float CurrentQuadraticValue => CurrentLinearValue * CurrentLinearValue;
        public float CurrentValue
        {
            get
            {
                switch (DefaultInterpolationValueType)
                {
                    case AeInterpolationDefaultValueType.Linear:
                        return CurrentLinearValue;
                    case AeInterpolationDefaultValueType.Quadratic:
                        return CurrentQuadraticValue;
                    default:
                        // TODO : Release mode doesn't crash
                        throw new Exception("Unhandle enum value");
                }
            }
        }

        public bool IsRunning { get; private set; } = false;
        public event AeEventOnInterpolationComplete OnInterpolationComplete;
        public int CurrentTarget { get; private set; }
        public AeInterpolationDefaultValueType DefaultInterpolationValueType { get; set; } = AeInterpolationDefaultValueType.Linear;

        public void Start(int milliseconds, AeInterpolationType interpolationType = AeInterpolationType.OneShot)
        {
            if (!IsRunning)
            {
                CurrentTarget = milliseconds;
                _currentTime = 0;
                _interpolationType = interpolationType;
                IsRunning = true;
            }
            else
            {
                //throw new Exception("This interpolator is already running, should this be an error?");
            }
        }

        public void Kill(bool shouldFileCompleteEvent = true)
        {
            if (IsRunning)
            {
                IsRunning = false;
                if (shouldFileCompleteEvent)
                {
                    OnInterpolationComplete?.Invoke(this);
                }
            }
        }

        public void ClearEvents()
        {
            OnInterpolationComplete = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsRunning)
            {
                switch (_interpolationType)
                {
                    case AeInterpolationType.OneShot:
                        _currentTime += gameTime.ElapsedGameTime.Milliseconds;
                        if (_currentTime >= CurrentTarget)
                        {
                            IsRunning = false;
                            OnInterpolationComplete?.Invoke(this);
                        }
                        break;
                    case AeInterpolationType.ContinuousSinWave:
                        if (_isTimerIncreasing)
                        {
                            _currentTime += gameTime.ElapsedGameTime.Milliseconds;
                        }
                        else
                        {
                            _currentTime -= gameTime.ElapsedGameTime.Milliseconds;
                        }

                        if (_currentTime >= CurrentTarget)
                        {
                            _currentTime = CurrentTarget;
                            _isTimerIncreasing = false;
                        }
                        if (_currentTime <= 0)
                        {
                            _currentTime = 0;
                            _isTimerIncreasing = true;
                        }
                        
                        OnInterpolationComplete?.Invoke(this);
                        break;
                    default:
                        break;
                }
                
            }
        }

        private AeInterpolationType _interpolationType = AeInterpolationType.OneShot;
        private bool _isTimerIncreasing = true;
        private int _currentTime;
    }
}
