using Aerolite;
using Aerolite.Subsystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AeroliteTestWindows
{
    public class EntryState : AeState
    {
        AeText _instructions;

        public EntryState()
        {
            var resources = TestGame.Resources;
            _instructions = new AeText("press space to go to spritetest", resources.FontGame);
            _instructions.Transform.X = 50;
            _instructions.Transform.Y = 50;
            AddEntity(_instructions);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Engine.StateManager.ChangeStateWithCurtains(new SpriteTestState());
            }
        }
    }
}
