using UnityEngine;
using Common.GenericStateMachine;

namespace Gameplay.GameManagerStateMachine
{
    public enum StateIds { None, LevelLoad, TapToStart, Game, LevelSuccess, LevelFail, StartNextLevel, RestartLevel }

    public class StateMachine : GenericStateMachine<StateIds, StateInfo>
    {

    }
}