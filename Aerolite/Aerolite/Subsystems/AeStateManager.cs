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

        /// <summary>
        /// state changes are usually done during update which means we would modify the state collection while
        /// we're iterating on it. So we defer all state manipulations until after all state updates are complete. 
        /// </summary>
        private List<Action> _stateManiulationDeferredOperations = new List<Action>();

        public AeStateManager()
        { }

        public void PushState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
           {
               _states.Add(state);
           });
        }

        public void PopState()
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                _states.RemoveAt(_states.Count - 1);
            });
        }

        public void PopState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                var indexOfStateToRemove = _states.FindIndex(x => x == state);
                if (indexOfStateToRemove != -1)
                {
                    _states.RemoveAt(indexOfStateToRemove);
                }
            });
        }

        public void ChangeState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                _states.Clear();
                _states.Add(state);
            });
        }



        public void Update(GameTime gameTime)
        {

            //states shouldn't bleed into each other which technically means 
            //I might be able to do this with a parallel for....
            for (int i = _states.Count - 1; i >= 0; --i)
            {
                _states[i].Update(gameTime);
            }
            foreach (var operation in _stateManiulationDeferredOperations)
            {
                operation.Invoke();
            }
            _stateManiulationDeferredOperations.Clear();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _states.Count; ++i)
            {
                _states[i].Draw(gameTime, spriteBatch);
            }
        }
    }
}
