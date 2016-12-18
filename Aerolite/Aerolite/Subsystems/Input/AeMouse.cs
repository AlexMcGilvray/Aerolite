using Aerolite.Subsystems.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeMouseData
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class AeMouse
    {
        private MouseState _deviceCoordinates;
        private MouseState _previousDeviceCoordinates;

        private AeMouseData _mouseData = new AeMouseData();

        private AeGraphics _graphics;

        public AeMouse(AeGraphics graphics)
        {
            _deviceCoordinates = Mouse.GetState();
            _previousDeviceCoordinates = Mouse.GetState();
            _graphics = graphics;
        }

        internal void Poll()
        {
            _previousDeviceCoordinates = _deviceCoordinates;
            _deviceCoordinates = Mouse.GetState();

            _mouseData.X = (int)(_deviceCoordinates.X * (_graphics.GraphicsSettings.GameResolutionWidth / ((float)_graphics.GraphicsSettings.ScreenResolutionWidth)));
            _mouseData.Y = (int)(_deviceCoordinates.Y * (_graphics.GraphicsSettings.GameResolutionHeight/ ((float)_graphics.GraphicsSettings.ScreenResolutionHeight)));
        }

        private MouseState State { get { return _deviceCoordinates; } }
        public int X { get { return _mouseData.X; } }
        public int Y { get { return _mouseData.Y; } }
        public int XDelta { get { return _deviceCoordinates.X - _previousDeviceCoordinates.X; } }
        public int YDelta { get { return _deviceCoordinates.Y - _previousDeviceCoordinates.Y; } }

        public bool LeftClick
        {
            get
            {
                if ((_deviceCoordinates.LeftButton == ButtonState.Pressed) && (_previousDeviceCoordinates.LeftButton == ButtonState.Released))
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
                if ((_deviceCoordinates.LeftButton == ButtonState.Pressed) && (_previousDeviceCoordinates.LeftButton == ButtonState.Pressed))
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
                if ((_deviceCoordinates.LeftButton == ButtonState.Released) && (_previousDeviceCoordinates.LeftButton == ButtonState.Pressed))
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
