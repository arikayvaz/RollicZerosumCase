using UnityEngine;
using Common.GenericStateMachine;

namespace Gameplay.GameManagerStateMachine
{
    public enum StateIds { None, LevelLoad, TapToStart, Game }

    public class StateMachine : GenericStateMachine<StateIds, StateInfo>
    {

    }
}