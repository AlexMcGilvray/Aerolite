using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aerolite.HighLevel2D;
using Aerolite.Entity;

namespace AeroliteTestGame
{
    class TestState : AeState
    {
        AeSprite _sprite;
        AeSprite _sprite2;
        AeSprite _sprite3;

        AeLayer _spriteLayer = new AeLayer();

        public TestState()
        {
            _sprite = new AeSprite("building_test");
            _sprite2 = new AeSprite("ground_001");
            _sprite3 = new AeSprite("ground_002");

            _sprite2.Transform.Y = 50;
            _sprite3.Transform.Y = 90;

            _spriteLayer.Add(_sprite);
            _spriteLayer.Add(_sprite2);
            _spriteLayer.Add(_sprite3);
            
            AddEntity(_spriteLayer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var keyboard = Engine.Input.Keyboard;

            if (keyboard.Down(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _sprite3.Transform.Y += 0.07f * gameTime.ElapsedGameTime.Milliseconds;
            } 
            else if (keyboard.Down(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _sprite3.Transform.Y -= 0.07f * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (keyboard.Down(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _sprite3.Transform.X -= 0.07f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (keyboard.Down(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _sprite3.Transform.X += 0.07f * gameTime.ElapsedGameTime.Milliseconds;
            }
            _sprite.Transform.X += 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            _sprite2.Transform.X += 0.05f * gameTime.ElapsedGameTime.Milliseconds;
            
        }

        public override void Draw(GameTime gameTime,SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
        }
    }
}
