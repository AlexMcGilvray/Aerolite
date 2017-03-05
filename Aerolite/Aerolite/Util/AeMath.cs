using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Util
{
    public static class AeMath
    {
        public static byte GetTweenValue(byte source, byte target,float interpolationValue)
        {
            return (byte)(source + (target - source) * interpolationValue);
        }

        public static float GetTweenValue(float source, float target, float interpolationValue)
        {
            return source + (target - source) * interpolationValue;
        }

        public static float Clamp (float value ,float lowerBound, float upperBound)
        {
            Debug.Assert(lowerBound < upperBound);
            if (value > upperBound)
            {
                value = upperBound;
            }
            if (value < lowerBound)
            {
                value = lowerBound;
            }
            return value;
        }
    }
}
