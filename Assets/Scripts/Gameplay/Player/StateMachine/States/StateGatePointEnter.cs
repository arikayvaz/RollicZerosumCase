using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.PlayerStateMachine
{
    public class StateGatePointEnter : StateBase
    {
        public override StateIds StateId => StateIds.GatePointEnter;

        private RigidbodyConstraints rigidbodyConstraints;

        public StateGatePointEnter(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            if (!info.collectionController.HasAnyCollectible) 
            {
                OnGateFail();
                return;
            }

            FreezeRigidbody(info);

            info.collectionController.PushCollectibles();
            info.currentGatePoint.StartGateOperation((success) => OnGateOperationComplete(success, info));
        }

        private void OnGateOperationComplete(bool isSuccess, StateInfo info) 
        {
            info.currentGatePoint = null;
            ResetRigidbody(info);

            if (isSuccess)
                OnGateSuccess();
            else
                OnGateFail();
        }

        private void FreezeRigidbody(StateInfo info) 
        {
            rigidbodyConstraints = info.rb.constraints;
            info.rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void ResetRigidbody(StateInfo info) 
        {
            info.rb.constraints = rigidbodyConstraints;
        }

        private void OnGateSuccess() 
        {
            Debug.Log("Operation Succeed!");
        }

        private void OnGateFail() 
        {
            Debug.Log("Operation Failed!");
        }
    }
}