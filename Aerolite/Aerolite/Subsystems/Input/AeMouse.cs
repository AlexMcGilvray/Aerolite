using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeMouse
    {
        private MouseState _mouseState;
        private MouseState _prevMouseState;

        public AeMouse()
        {
            _mouseState = Mouse.GetState();
            _prevMouseState = Mouse.GetState();
        }

        internal void Poll()
        {
            _prevMouseState = _mouseState;
            _mouseState = Mouse.GetState();
        }

        private MouseState State { get { return _mouseState; } }
        public int X { get { return _mouseState.X; } }
        public int Y { get { return _mouseState.Y; } }
        public int XDelta { get { return _mouseState.X - _prevMouseState.X; } }
        public int YDelta { get { return _mouseState.Y - _prevMouseState.Y; } }

        public bool LeftClick
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Pressed) && (_prevMouseState.LeftButton == ButtonState.Released))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsHoldingLeftButton
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Pressed) && (_prevMouseState.LeftButton == ButtonState.Pressed))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasReleasedLeftButton
        {
            get
            {
                if ((_mouseState.LeftButton == ButtonState.Released) && (_prevMouseState.LeftButton == ButtonState.Pressed))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
