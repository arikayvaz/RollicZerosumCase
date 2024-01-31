using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateGame : StateBase
    {
        public override StateIds StateId => StateIds.Game;

        public StateGame(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            InputManager.Instance.ResetInput();
            PlayerController.Instance.StartMoving();
        }
    }
}
