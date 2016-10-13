using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems
{
    public class AeStateManager
    {
        private List<AeState> _states = new List<AeState>();
        private SpriteBatch _spriteBatch;

        public AeStateManager(SpriteBatch batch)
        {
            _spriteBatch = batch;
        }

        public void Add(AeState state)
        {
            _states.Add(state);
        }
        
        public void Update(GameTime gameTime)
        {
            //states shouldn't bleed into each other which technically means 
            //I might be able to do this with a parallel for....
            foreach(AeState state in _states)
            {
                state.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (AeState state in _states)
            {
                state.Draw(gameTime, _spriteBatch);
            }
        }
    }
}
