using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Util.Interpolators
{
    internal enum AeInterpolationDirection
    {
        Forward,
        Backward
    }

    public class AeInterpolator //this class is a good example of needing a IUpdatable interface. Entity is relatively too heavy
    {
        public int TimeLength
        {
            get
            {
                return _timeLength;
            }
            set
            {
                _timeLength = value;
                _currentTime = (int)((AeEngine.Singleton().Random.GetFloat() * TimeLength * 2) - TimeLength);
            }
        }
        public float CurrentValue
        {
            get
            {
                return ((float)_currentTime / TimeLength);
            }
        }

        private int _currentTime = 0;
        private int _timeLength = 3000;
        private AeInterpolationDirection _interpolationDirection = AeInterpolationDirection.Forward;

        public AeInterpolator()
        {
           
        }

        public void Update(GameTime gameTime)
        {
            if (_interpolationDirection == AeInterpolationDirection.Forward)
            {
                _currentTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_currentTime > TimeLength)
                {
                    _interpolationDirection = AeInterpolationDirection.Backward;
                    _currentTime = TimeLength;
                }
            }
            else
            {
                _currentTime -= gameTime.ElapsedGameTime.Milliseconds;
                if (_currentTime < -TimeLength)
                {
                    _interpolationDirection = AeInterpolationDirection.Forward;
                    _currentTime = -TimeLength;
                }
            }
        }
    }
}
