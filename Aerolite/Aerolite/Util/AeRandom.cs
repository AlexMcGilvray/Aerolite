﻿using System;
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

        public Random RandomSource { get { return _random; } }

        public float GetFloat()
        {
            return (float)_random.NextDouble();
        }

        public bool CoinToss()
        {
            return GetFloat() > 0.5f ? true : false;
        }
    }
}
