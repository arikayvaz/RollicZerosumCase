using UnityEngine;
using Common;
using GameStates = Gameplay.GameManagerStateMachine.StateIds;

namespace Gameplay
{
    public class UIController : Singleton<UIController>, IController
    {
        [SerializeField] TapToStartUI tapToStart = null;
        [SerializeField] GameUI game = null;
        [SerializeField] LevelSuccessUI levelSuccess = null;
        [SerializeField] LevelFailUI levelFail = null;

        private GameStates currentGateState = GameStates.None;

        public void InitController()
        {
            InitAllUI();
            HideAllUI();
        }

        public void OnGameStateChanged(GameStates newState) 
        {
            UpdateCurrentUIPanel(newState);
            currentGateState = newState;
        }

        private void UpdateCurrentUIPanel(GameStates newState) 
        {
            switch (currentGateState)
            {
                case GameStates.TapToStart:
                    tapToStart.HideUI();
                    break;
                case GameStates.Game:
                    game.HideUI();
                    break;
                case GameStates.LevelSuccess:
                    levelSuccess.HideUI();
                    break;
                case GameStates.LevelFail:
                    levelFail.HideUI();
                    break;
            }

            switch (newState)
            {
                case GameStates.TapToStart:
                    tapToStart.ShowUI();
                    break;
                case GameStates.Game:
                    game.ShowUI();
                    break;
                case GameStates.LevelSuccess:
                    levelSuccess.ShowUI();
                    break;
                case GameStates.LevelFail:
                    levelFail.ShowUI();
                    break;
            }
        }

        private void HideAllUI() 
        {
            tapToStart.HideUI();
            game.HideUI();
            levelSuccess.HideUI();
            levelFail.HideUI();
        }

        private void InitAllUI() 
        {
            tapToStart.InitUI();
            game.InitUI();
            levelSuccess.InitUI();
            levelFail.InitUI();
        }
    }
}