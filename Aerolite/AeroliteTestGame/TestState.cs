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

        public TestState()
        {
            _sprite = new AeSprite("building_test");
        }
           
        public override void Update(GameTime gameTime)
        {
            _sprite.Transform.X += 0.1f * gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void Draw(GameTime gameTime,SpriteBatch batch)
        {
            
            _sprite.Draw(gameTime, batch);
        }
    }
}
