using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeKeyboard
    {
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;

        public AeKeyboard()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public bool Pressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key));
        }

        public bool IsAnyKeyDown()
        {
            if (keyboardState.GetPressedKeys().Length > 0)
            {
                return true;
            }
            else { return false; }
        }

        public bool Down(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public bool Released(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key));
        }

        internal void Poll()
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

    }
}
