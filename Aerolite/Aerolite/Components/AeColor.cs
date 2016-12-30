using Aerolite.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public class AeColor : AeComponent
    {
        Color _currentColor;
        Color _targetColor;
        Color _baseColor;

        public Color CurrentColor => _currentColor;
        public Color TargetColor => _targetColor;
        public Color BaseColor => _baseColor;
        public bool IsInterpolating => _colorInterpolator.IsRunning;

        private AeInterpolator _colorInterpolator = new AeInterpolator();

        public AeColor()
            :this(Color.White)
        { }

        public AeColor(Color color)
        {
            _currentColor = color;
            _baseColor = color;
            _targetColor = color;
            this.AddComponent(_colorInterpolator);
        }

        public void SetColor(Color color)
        {
            _currentColor = color;
        }

        public void InterpololateTo(Color targetColor,int milliseconds,bool switchBackOnComplete)
        {
            _targetColor = targetColor;
            _colorInterpolator.Start(milliseconds);
            _colorInterpolator.ClearEvents();
            if (switchBackOnComplete)
            {
                _colorInterpolator.OnInterpolationComplete += interpoloator => _currentColor = _baseColor;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_colorInterpolator.IsRunning)
            {
                float currentInterpolatorValue = _colorInterpolator.CurrentLinearValue;
                _currentColor.R = AeMath.GetTweenValue(_baseColor.R, _targetColor.R, currentInterpolatorValue);
                _currentColor.G = AeMath.GetTweenValue(_baseColor.G, _targetColor.G, currentInterpolatorValue);
                _currentColor.B = AeMath.GetTweenValue(_baseColor.B, _targetColor.B, currentInterpolatorValue);
            }
        }
    }
}