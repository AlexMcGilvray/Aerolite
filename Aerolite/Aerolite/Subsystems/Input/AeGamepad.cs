using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeGamepad
    {
        private GamePadState[] prevGamePadsState = new GamePadState[4];
        private GamePadState[] gamePadsState = new GamePadState[4];

        public GamePadState[] Player
        {
            get
            {
                return (gamePadsState);
            }
        }

        public AeGamepad()
        {
            prevGamePadsState[0] = GamePad.GetState(PlayerIndex.One);
            prevGamePadsState[1] = GamePad.GetState(PlayerIndex.Two);
            prevGamePadsState[2] = GamePad.GetState(PlayerIndex.Three);
            prevGamePadsState[3] = GamePad.GetState(PlayerIndex.Four);
        }

        internal void Poll()
        {
            //set our previous state to our new state
            prevGamePadsState[0] = gamePadsState[0];
            prevGamePadsState[1] = gamePadsState[1];
            prevGamePadsState[2] = gamePadsState[2];
            prevGamePadsState[3] = gamePadsState[3];

            //get our new state
            gamePadsState[0] = GamePad.GetState(PlayerIndex.One);
            gamePadsState[1] = GamePad.GetState(PlayerIndex.Two);
            gamePadsState[2] = GamePad.GetState(PlayerIndex.Three);
            gamePadsState[3] = GamePad.GetState(PlayerIndex.Four);
        }

        public bool Pressed(int playerIndex, Buttons button)
        {
            return (gamePadsState[playerIndex - 1].IsButtonDown(button) &&
                prevGamePadsState[playerIndex - 1].IsButtonUp(button));
        }

        public bool Released(int playerIndex, Buttons button)
        {
            return (gamePadsState[playerIndex - 1].IsButtonUp(button) &&
                prevGamePadsState[playerIndex - 1].IsButtonDown(button));
        }

        public bool Down(int playerIndex, Buttons button)
        {
            return (gamePadsState[playerIndex - 1].IsButtonDown(button));
        }
    }
}
