using Aerolite.Subsystems.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeInput
    {
        private AeKeyboard _keyboard;
        private AeGamepad _gamepads;
        private AeMouse _mouse;
        private AeTouch _touch;
        public bool MouseEnable { get; set; }

        public AeInput(AeGraphics graphics)
        {
            _keyboard = new AeKeyboard();
            _mouse = new AeMouse(graphics);
            _gamepads = new AeGamepad();
            _touch = new AeTouch();
        }

        public AeGamepad GamePads
        {
            get { return _gamepads; }
        }

        public AeKeyboard Keyboard
        {
            get { return _keyboard; }
        }

        public AeMouse Mouse
        {
            get { return _mouse; }
        }

        public AeTouch Touch
        {
            get { return _touch; }
        }

        public void PollInput()
        {
            _gamepads.Poll();
            _keyboard.Poll();
            _mouse.Poll();
            _touch.Poll();
        }
    }
}
