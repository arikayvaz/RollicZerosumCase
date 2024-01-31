using Common.GenericStateMachine;

namespace Gameplay.PlayerStateMachine
{
    public abstract class StateBase : GenericStateBase<StateIds, StateInfo>
    {
        public StateBase(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }
    }
}