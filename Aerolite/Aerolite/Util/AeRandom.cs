using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Util
{
    public class AeRandom
    {
        private Random _random;

        public AeRandom()
        {
            _random = new Random();
        }

        public float GetFloat()
        {
            return (float)_random.NextDouble();
        }
    }
}
