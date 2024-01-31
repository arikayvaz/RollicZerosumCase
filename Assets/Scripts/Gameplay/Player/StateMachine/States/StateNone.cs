using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.PlayerStateMachine
{
    public class StateNone : StateBase
    {
        public override StateIds StateId => StateIds.None;

        public StateNone(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}