using Common.GenericStateMachine;

namespace Gameplay.PlayerStateMachine
{
    [System.Serializable]
    public class StateFactory : GenericStateFactory<StateIds, StateInfo, StateMachine>
    {
        public override GenericStateBase<StateIds, StateInfo> CreateState(StateIds stateId, StateMachine stateMachine)
        {
            switch (stateId)
            {
                case StateIds.None:
                    return new StateNone(stateMachine);
                case StateIds.Idle:
                    return new StateIdle(stateMachine);
                case StateIds.Move:
                    return new StateMove(stateMachine);
                case StateIds.GatePointEnter:
                    return new StateGatePointEnter(stateMachine);
                default:
                    return null;
            }
        }
    }
}