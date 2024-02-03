using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.GameManagerStateMachine
{
    public class StateLevelSuccess : StateBase
    {
        public override StateIds StateId => StateIds.LevelSuccess;

        public StateLevelSuccess(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            Debug.Log("State Level Success");
        }
    }
}
