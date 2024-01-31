using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateTapToStart : StateBase
    {
        public override StateIds StateId => StateIds.TapToStart;

        public StateTapToStart(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            InputManager.Instance.OnStartScreenInputClicked += OnStartScreenInputClicked;
        }

        public override void OnExit(StateInfo info)
        {
            base.OnExit(info);

            InputManager.Instance.OnStartScreenInputClicked -= OnStartScreenInputClicked;
        }

        private void OnStartScreenInputClicked() 
        {
            stateMachine.ChangeState(StateIds.Game);
        }
    }
}
