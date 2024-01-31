using Common.GenericStateMachine;

namespace Gameplay.PlayerStateMachine
{
    public enum StateIds { None, Idle, Move }

    public class StateMachine : GenericStateMachine<StateIds, StateInfo>
    {

    }
}