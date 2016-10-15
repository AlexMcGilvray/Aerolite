using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AeroliteTestGame
{
    class TestState : AeState
    {

        Texture2D someTexture;
        Vector2 pos;
        
        public TestState()
        {
            // this is a little roundabout to get to content. Maybe give engine a property
            // edit we should be using texture manager anyways
            someTexture = Engine.GameReference.Content.Load<Texture2D>("building_test");
            
        }
           
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime,SpriteBatch batch)
        {
            


            batch.Draw(someTexture, new Vector2(100, 100), Color.White);
          
        }
    }
}
