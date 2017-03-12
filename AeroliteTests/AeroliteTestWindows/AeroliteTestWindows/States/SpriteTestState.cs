using Aerolite;
using Aerolite.Components;
using Aerolite.HighLevel2D;
using Aerolite.Subsystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AeroliteTestWindows
{
    class SpriteTestState : AeState
    {
        AeText _instructions;

        AeSprite _sprite;

        public SpriteTestState()
        {
            var resources = TestGame.Resources;

            _instructions = new AeText("press space to go to spritetest", resources.FontGame);
            _instructions.Transform.X = 50;
            _instructions.Transform.Y = 50;
            AddEntity(_instructions);

            _sprite = new AeSprite();

            _sprite.Animator.Add("idle",new AeAnimation("player_ship_ethervoyager",_sprite.Animator,new AeAnimationFrame[] { new AeAnimationFrame(0, 0, 64, 64, 100) }));
            _sprite.Transform.X = 50;
            _sprite.Transform.Y = 50;

            AddEntity(_sprite);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _sprite.Transform.X -= 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _sprite.Transform.X += 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _sprite.Transform.Y -= 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _sprite.Transform.Y += 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.E)
            {
                _sprite.Transform.Orientation -= 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Input.Keyboard.Down(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                _sprite.Transform.Orientation += 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            }
        }

    }
}
