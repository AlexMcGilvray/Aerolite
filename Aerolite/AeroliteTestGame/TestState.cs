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
using Aerolite.Components;

namespace AeroliteTestGame
{
    class TestState : AeState
    {
        AeSprite _sprite;
        AeSprite _sprite2;
        AeSprite _sprite3;

        AeSprite _backgroundSky;

        AeSpriteLayer _spriteLayer = new AeSpriteLayer();

        public TestState()
        {
            _backgroundSky = new AeSprite("background_sky01");
            _sprite = new AeSprite("building_test");
            _sprite2 = new AeSprite("ground_001");
            _sprite3 = new AeSprite();
            
            AeAnimation animation = new AeAnimation(
                "ground_002-sheet", 
                _sprite3.Animator, 
                new AeAnimationFrame[]{
                    new AeAnimationFrame(0, 0, 128, 64, 100),
                    new AeAnimationFrame(128, 0, 128, 64, 150),
                    new AeAnimationFrame(256, 0, 128, 64, 200)
            });

            _sprite3.Animator.Add("idle",animation);
            
            _sprite2.Transform.Y = 50;
            _sprite3.Transform.Y = 90;

            _spriteLayer.Add(_sprite);
            _spriteLayer.Add(_sprite2);
            _spriteLayer.Add(_sprite3);
            
            AddEntity(_backgroundSky);
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
            _sprite.Transform.X += 0.01f * gameTime.ElapsedGameTime.Milliseconds;
            _sprite2.Transform.X += 0.005f * gameTime.ElapsedGameTime.Milliseconds;

            _backgroundSky.Transform.X -= 0.01f * gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void Draw(GameTime gameTime,SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
        }
    }
}
