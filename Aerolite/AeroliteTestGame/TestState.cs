using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aerolite.HighLevel2D;

namespace AeroliteTestGame
{
    class TestState : AeState
    {
        AeSprite _sprite;
        AeSprite _sprite2;
        AeSprite _sprite3;

        public TestState()
        {
            _sprite = new AeSprite("building_test");
            _sprite2 = new AeSprite("ground_001");
            _sprite3 = new AeSprite("ground_002");

            _sprite2.Transform.Y = 50;
            _sprite3.Transform.Y = 90;

        }

        public override void Update(GameTime gameTime)
        {
            _sprite.Transform.X += 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            _sprite2.Transform.X += 0.05f * gameTime.ElapsedGameTime.Milliseconds;

            _sprite3.Transform.X += 0.07f * gameTime.ElapsedGameTime.Milliseconds;

        }

        public override void Draw(GameTime gameTime,SpriteBatch batch)
        {
            _sprite.Draw(gameTime, batch);
            _sprite2.Draw(gameTime, batch);
            _sprite3.Draw(gameTime, batch);
        }
    }
}
