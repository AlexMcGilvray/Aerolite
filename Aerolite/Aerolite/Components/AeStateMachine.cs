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

        public void AddEnterFunction(T state, AeStateMachineOnExit exitFunction)
        {
            _stateExitFunctions.Add(state, exitFunction);
        }
    }
}
