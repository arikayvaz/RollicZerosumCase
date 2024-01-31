using UnityEngine;
using Common.GenericStateMachine;

namespace Gameplay.GameManagerStateMachine
{
    public abstract class StateBase : GenericStateBase<StateIds, StateInfo>
    {
        protected StateBase(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}