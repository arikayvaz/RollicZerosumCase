using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateLevelLoad : StateBase
    {
        public override StateIds StateId => StateIds.LevelLoad;

        public StateLevelLoad(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}
