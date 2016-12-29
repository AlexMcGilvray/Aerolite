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

        private byte GetInterpolatedColorValue(byte source, byte target)
        {
            return (byte)(source + (target - source) * _colorInterpolator.CurrentValue);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_colorInterpolator.IsRunning)
            {
                //byte red = (byte)((_baseColor.R * (1 / _colorInterpolator.CurrentValue)) + (_targetColor.R * _colorInterpolator.CurrentValue));
                //byte green = (byte)((_baseColor.G * (1 / _colorInterpolator.CurrentValue)) + (_targetColor.G * _colorInterpolator.CurrentValue));
                //byte blue = (byte)((_baseColor.B * (1 / _colorInterpolator.CurrentValue)) + (_targetColor.B * _colorInterpolator.CurrentValue));

                byte red = GetInterpolatedColorValue(_baseColor.R, _targetColor.R);
                byte green = GetInterpolatedColorValue(_baseColor.G, _targetColor.G);
                byte blue = GetInterpolatedColorValue(_baseColor.B, _targetColor.B);

                _currentColor.R = red;
                _currentColor.G = green;
                _currentColor.B = blue;
            }
        }
    }
}
