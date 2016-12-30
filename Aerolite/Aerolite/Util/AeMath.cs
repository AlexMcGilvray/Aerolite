using System;
using System.Collections.Generic;
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
    }
}
