using Common.GenericStateMachine;
using LevelScene;
using UnityEngine;
using User;

namespace Gameplay.GameManagerStateMachine
{
    public class StateStartNextLevel : StateBase
    {
        public override StateIds StateId => StateIds.StartNextLevel;

        public StateStartNextLevel(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            UserManager.Instance.IncreaseLevelNoAndSave();
            LevelSceneManager.LoadScene();
        }
    }
}
