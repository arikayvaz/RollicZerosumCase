using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateLevelFail : StateBase
    {
        public override StateIds StateId => StateIds.LevelFail;

        public StateLevelFail(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}
