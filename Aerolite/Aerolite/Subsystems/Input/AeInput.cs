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
        //private GamePadHandler gamepads;
        //private MouseHandler mouse;
        public bool MouseEnable { get; set; }

        public AeInput()
        {
            keyboard = new AeKeyboard();
            //mouse = new MouseHandler();
            //gamepads = new GamePadHandler();
        }

        //public GamePadHandler GamePads
        //{
        //    get { return gamepads; }
        //}

        public AeKeyboard Keyboard
        {
            get { return keyboard; }
        }

        //public MouseHandler Mouse
        //{
        //    get { return mouse; }
        //}

        public void PollInput()
        {
            //gamepads.Poll();
            keyboard.Poll();
            //mouse.Poll();
        }
    }
}
