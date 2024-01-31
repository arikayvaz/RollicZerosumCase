using UnityEngine;
using Common;
using System.Security.Cryptography;

namespace Gameplay
{
    public class InputManager : Singleton<InputManager>
    {
        public float TargetX { get; private set; }

        private bool _isTouchScreen;
        private float _startX;

        protected override void Awake()
        {
            base.Awake();

            _isTouchScreen = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

            _startX = 0f;
            TargetX = 0f;
        }

        private void Update()
        {
            if (GameManager.Instance.IsStartScreen) 
            {
                HandleStartScreenInput();
                return;
            }

            if (GameManager.Instance.IsGameScreen)
            {
                HandleGameplayInput();
                return;
            }
        }

        public void ResetInput() 
        {
            TargetX = 0f;
            _startX = 0f;
        }

        private void HandleStartScreenInput() 
        {

        }

        private void HandleGameplayInput() 
        {
            if (_isTouchScreen)
                CheckTouchScreenInput();
            else
                CheckMouseInput();
        }

        private void CheckMouseInput() 
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnInputStart(Input.mousePosition.x);
                return;
            }

            if (Input.GetMouseButton(0)) 
            {
                OnInputUpdate(Input.mousePosition.x);
                return;
            }

            if (Input.GetMouseButtonUp(0))
                OnInputEnd();


        }

        private void CheckTouchScreenInput() 
        {
            if (Input.touchCount <= 0)
                return;

            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    OnInputStart(Input.GetTouch(0).position.x);
                    break;
                case TouchPhase.Moved:
                    OnInputUpdate(Input.GetTouch(0).position.x);
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    OnInputEnd();
                    break;
            }
        }

        private void OnInputStart(float x) 
        {
            _startX = x;
        }

        private void OnInputUpdate(float x) 
        {
            UpdateTargetX(x);
        }

        private void OnInputEnd() 
        {

        }


        const float SCREEN_WORLD_MULT = 100f;

        private void UpdateTargetX(float x) 
        {
            //TODO: Get Level Settings
            float roadHalfWidth = GameManager.GameSettings.roadHalfWidth;

            if (Mathf.Abs(x - _startX) > 0.01f)
            {
                TargetX += (x - _startX) * SCREEN_WORLD_MULT / Screen.width;
                TargetX = Mathf.Clamp(TargetX, -roadHalfWidth, roadHalfWidth);
            }

            _startX = x;
        }
    }
}