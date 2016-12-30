using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public delegate void AeStateMachineUpdate(GameTime gameTime);
    public delegate void AeStateMachineOnEnter();
    public delegate void AeStateMachineOnExit();

    /// <summary>
    /// Possible additions
    ///  * Add transition rules so you can enforce what states can change to each other etc
    ///  * Stateful enter/exit functions (aka instead of oneshots they run each update until some condition is met)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AeStateMachine<T> : AeComponent where T : struct
    {
        private Dictionary<T, AeStateMachineUpdate> _stateUpdateFunctions = new Dictionary<T, AeStateMachineUpdate>();
        private Dictionary<T, AeStateMachineOnEnter> _stateEnterFunctions = new Dictionary<T, AeStateMachineOnEnter>();
        private Dictionary<T, AeStateMachineOnExit> _stateExitFunctions = new Dictionary<T, AeStateMachineOnExit>();

        public T CurrentState { get; private set; }

        public AeStateMachine()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic type argument for AeStateMachine must be System.Enum");
            }
            //var enumValues = Enum.GetValues(typeof(T)).OfType<T>();
        }

        public void AddUpdateFunction(T state, AeStateMachineUpdate updateFunction)
        {
            _stateUpdateFunctions.Add(state, updateFunction);
        }

        public void AddEnterFunction(T state, AeStateMachineOnEnter enterFunction)
        {
            _stateEnterFunctions.Add(state, enterFunction);
        }

        public void AddExitFunction(T state, AeStateMachineOnExit exitFunction)
        {
            _stateExitFunctions.Add(state, exitFunction);
        }

        public void ChangeState(T state)
        {
            if (_stateExitFunctions.ContainsKey(state))
            {
                _stateExitFunctions[state].Invoke();
            }
            if (_stateEnterFunctions.ContainsKey(state))
            {
                _stateEnterFunctions[state].Invoke();
            }
            CurrentState = state;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_stateUpdateFunctions.ContainsKey(CurrentState))
            {
                _stateUpdateFunctions[CurrentState].Invoke(gameTime);
            }
        }
    }
}
