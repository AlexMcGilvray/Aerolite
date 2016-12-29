using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Aerolite.Components
{
    public delegate void AeEventOnInterpolationComplete(AeInterpolator interpolator);

    public class AeInterpolator : AeComponent
    {
        public float CurrentValue { get { return _currentTime / (float)CurrentTarget; } }
        public bool IsRunning { get; private set; } = false;

        public event AeEventOnInterpolationComplete OnInterpolationComplete;

        public int CurrentTarget { get; private set; }
        private int _currentTime;


        public void Start(int milliseconds)
        {
            if (!IsRunning)
            {
                CurrentTarget = milliseconds;
                _currentTime = 0;
                IsRunning = true;
            }
            else
            {
                //throw new Exception("This interpolator is already running, should this be an error?");
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
                _currentTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_currentTime >= CurrentTarget)
                {
                   
                    IsRunning = false;
                    OnInterpolationComplete?.Invoke(this);
                }
            }
        }
    }
}
