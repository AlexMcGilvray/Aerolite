using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeInput
    {
        private AeKeyboard keyboard;
        private AeGamepad gamepads;
        private AeMouse mouse;
        public bool MouseEnable { get; set; }

        public AeInput()
        {
            keyboard = new AeKeyboard();
            mouse = new AeMouse();
            gamepads = new AeGamepad();
        }

        public AeGamepad GamePads
        {
            get { return gamepads; }
        }

        public AeKeyboard Keyboard
        {
            get { return keyboard; }
        }

        public AeMouse Mouse
        {
            get { return mouse; }
        }

        public void PollInput()
        {
            gamepads.Poll();
            keyboard.Poll();
            mouse.Poll();
        }
    }
}
