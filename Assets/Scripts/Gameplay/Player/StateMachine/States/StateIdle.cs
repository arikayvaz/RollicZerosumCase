using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.PlayerStateMachine
{
    public class StateIdle : StateBase
    {
        public override StateIds StateId => StateIds.Idle;

        public StateIdle(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}