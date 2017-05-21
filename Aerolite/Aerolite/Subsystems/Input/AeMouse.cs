using Aerolite.Subsystems.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public struct AeMouseData
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class AeMouse
    {
        private MouseState _mouseState;
        private MouseState _previousMouseState;

        private AeMouseData _mouseData;

        private AeGraphics _graphics;

        public AeMouse(AeGraphics graphics)
        {
            _mouseState = Mouse.GetState();
            _previousMouseState = Mouse.GetState();
            _graphics = graphics;
        }

        internal void Poll()
        {
            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            _mouseData.X = (int)(_mouseState.X * (_graphics.GraphicsSettings.GameResolutionWidth / ((float)_graphics.GraphicsSettings.ScreenResolutionWidth)));
            _mouseData.Y = (int)(_mouseState.Y * (_graphics.GraphicsSettings.GameResolutionHeight / ((float)_graphics.GraphicsSettings.ScreenResolutionHeight)));
        }

        private MouseState State { get { return _mouseState; } }
        public int X { get { return _mouseData.X; } }
        public int Y { get { return _mouseData.Y; } }
        public int XDelta { get { return _mouseState.X - _previousMouseState.X; } }
        public int YDelta { get { return _mouseState.Y - _previousMouseState.Y; } }
        public AeMouseData MouseData { get { return _mouseData; } }

        public bool LeftClick
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Pressed) && (_previousMouseState.LeftButton == ButtonState.Released))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsHoldingLeftButton
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Pressed) && (_previousMouseState.LeftButton == ButtonState.Pressed))
                {
                    return true;
                }
                return false;
            }
        }

        public bool HasReleasedLeftButton
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Released) && (_previousMouseState.LeftButton == ButtonState.Pressed))
                {
                    return true;
                }
                return false;
            }
        }

        public bool MouseWheelScrolledUp
        {
            get
            {
                if (_mouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue < 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool MouseWheelScrolledDown
        {
            get
            {
                if (_mouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
