using Common.GenericStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.PlayerStateMachine
{
    public class StateMove : StateBase
    {
        public override StateIds StateId => StateIds.Move;

        public StateMove(GenericStateMachine<StateIds, StateInfo> stateMachine) : base(stateMachine) { }

        private float moveXLimit;

        public override void OnEnter(StateInfo info)
        {
            base.OnEnter(info);

            AdjustMoveXLimit(info);
        }

        public override void OnFixedUpdate(StateInfo info)
        {
            base.OnFixedUpdate(info);

            HandleMovement(info);
        }

        private void AdjustMoveXLimit(StateInfo info) 
        {
            float roadHalfWidth = GameManager.LevelSettings.roadHalfWidth;
            moveXLimit = roadHalfWidth - info.PlayerHalfSize;

            if (moveXLimit < 0f)
                moveXLimit = 0f;

        }

        private void HandleMovement(StateInfo info) 
        {
            float inputX = InputManager.Instance.TargetX;
            float adjustedX = Mathf.Clamp(inputX, -moveXLimit, moveXLimit);

            Vector3 pos = info.rb.position;

            pos.x = Mathf.Lerp(pos.x, adjustedX, info.MoveSpeed * Time.deltaTime);
            pos.z += info.MoveSpeed * Time.deltaTime;

            info.rb.position = pos;
        }
    }
}