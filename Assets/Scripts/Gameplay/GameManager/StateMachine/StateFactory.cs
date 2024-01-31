using UnityEngine;
using Common.GenericStateMachine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateFactory : GenericStateFactory<StateIds, StateInfo, StateMachine>
    {
        public override GenericStateBase<StateIds, StateInfo> CreateState(StateIds stateId, StateMachine stateMachine)
        {
            switch (stateId)
            {
                case StateIds.None:
                    return new StateNone(stateMachine);
                case StateIds.LevelLoad:
                    return new StateLevelLoad(stateMachine);
                case StateIds.TapToStart:
                    return new StateTapToStart(stateMachine);
                case StateIds.Game:
                    return new StateGame(stateMachine);
                default:
                    return null;
            }
        }

    }
}