using Common.GenericStateMachine;

namespace Gameplay.PlayerStateMachine
{
    public enum StateIds { None, Idle, Move, GatePointEnter }

    public class StateMachine : GenericStateMachine<StateIds, StateInfo>
    {

    }
}