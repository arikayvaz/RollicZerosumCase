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
                case StateIds.LevelSuccess:
                    return new StateLevelSuccess(stateMachine);
                case StateIds.LevelFail:
                    return new StateLevelFail(stateMachine);
                case StateIds.StartNextLevel:
                    return new StateStartNextLevel(stateMachine);
                case StateIds.RestartLevel:
                    return new StateRestartLevel(stateMachine);
                default:
                    return null;
            }
        }

    }
}