using Common.GenericStateMachine;
using UnityEngine;
using User;

namespace Gameplay.GameManagerStateMachine
{
    public class StateLevelLoad : StateBase
    {
        public override StateIds StateId => StateIds.LevelLoad;

        public StateLevelLoad(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            UserManager.Instance.LoadUserSave();

            LevelManager.Instance.OnLevelLoad();

            UIController.Instance.InitController();

            PlayerController.Instance.InitPlayer();

            GameCameraController.Instance.InitController(PlayerController.Instance.transform);
        }
    }
}
