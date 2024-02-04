using UnityEngine;
using Common;
using System.Security.Cryptography;
using System;

namespace Gameplay
{
    public class InputManager : Singleton<InputManager>
    {
        public float TargetX { get; private set; }

        private bool _isTouchScreen;
        private float _startX;
        private bool _isInputActive;

        public Action OnStartScreenInputClicked;

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
            _isInputActive = false;
        }

        private void HandleStartScreenInput() 
        {
            if (_isTouchScreen)
            {
                if (Input.touchCount <= 0)
                    return;

                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    OnStartScreenInputClicked?.Invoke();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnStartScreenInputClicked?.Invoke();
                }
            }
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
            _isInputActive = true;
        }

        private void OnInputUpdate(float x) 
        {
            if (!_isInputActive)
                return;

            UpdateTargetX(x);
        }

        private void OnInputEnd() 
        {
            _isInputActive = false;
        }


        const float SCREEN_WORLD_MULT = 5f;

        private void UpdateTargetX(float x) 
        {
            float roadHalfWidth = GameManager.LevelSettings.roadHalfWidth;

            if (Mathf.Abs(x - _startX) > 0.01f)
            {
                TargetX += (x - _startX) * SCREEN_WORLD_MULT / Screen.width;
                TargetX = Mathf.Clamp(TargetX, -roadHalfWidth, roadHalfWidth);
            }

            _startX = x;
        }
    }
}