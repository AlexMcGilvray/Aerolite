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

        public AeVectorTween(Vector2 source, Vector2 target)
        {
            _source = source;
            _target = target;
            _interpolator = new AeInterpolator();
        }

        public void Start(int tweenTimeMilliseconds = 3000)
        {
            _interpolator.Start(tweenTimeMilliseconds);
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
