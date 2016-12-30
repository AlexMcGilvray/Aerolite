using Aerolite.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components.Tweens
{
    public class AeVectorTween : AeComponent
    {
        private AeInterpolator _interpolator;
        private Vector2 _source;
        private Vector2 _target;

        public bool Complete { get; private set; } = false;

        public AeVectorTween()
        {
            _interpolator = new AeInterpolator();
            AddComponent(_interpolator);
        }

        public void Start(Vector2 source, Vector2 target,int tweenTimeMilliseconds = 3000)
        {
            _source = source;
            _target = target;
            _interpolator.Start(tweenTimeMilliseconds);
            Complete = false;
            _interpolator.OnInterpolationComplete += x => Complete = true;
        }

        public Vector2 GetCurrentValue()
        {
            Vector2 currentValue;
            currentValue.X = AeMath.GetTweenValue(_source.X, _target.X, _interpolator.CurrentLinearValue);
            currentValue.Y = AeMath.GetTweenValue(_source.Y, _target.Y, _interpolator.CurrentLinearValue);
            return currentValue;
        }
    }
}
