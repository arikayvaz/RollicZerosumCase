using Common.GenericStateMachine;
using LevelScene;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateRestartLevel : StateBase
    {
        public override StateIds StateId => StateIds.RestartLevel;

        public StateRestartLevel(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            LevelSceneManager.LoadScene();
        }
    }
}
