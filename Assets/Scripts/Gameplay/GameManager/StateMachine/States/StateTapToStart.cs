using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateTapToStart : StateBase
    {
        public override StateIds StateId => StateIds.TapToStart;

        public StateTapToStart(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}
