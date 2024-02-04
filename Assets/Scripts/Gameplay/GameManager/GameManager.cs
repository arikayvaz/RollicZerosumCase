using UnityEngine;
using Common;
using Gameplay.GameManagerStateMachine;

namespace Gameplay
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] StateInfo stateInfo = new StateInfo();
        [SerializeField] GameSettingsSO gameSettings = null;
        [SerializeField] LevelSettingsSO levelSettings = null;


        private StateMachine stateMachine;
        private StateFactory stateFactory;

        public static GameSettingsSO GameSettings => Instance?.gameSettings;
        public static LevelSettingsSO LevelSettings => Instance?.levelSettings;

        public StateIds CurrentState => stateMachine?.State?.StateId ?? StateIds.None;
        public bool IsStartScreen => CurrentState == StateIds.TapToStart;
        public bool IsGameScreen => CurrentState == StateIds.Game;

        protected override void Awake()
        {
            base.Awake();

            InitStateMachine();
        }

        private void Start()
        {
            LoadLevel();
            StartLevel();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            stateMachine.OnStateChanged -= OnGameStateChanged;
        }

        public void OnLevelSuccess() 
        {
            ChangeState(StateIds.LevelSuccess);
        }

        public void OnLevelFail() 
        {
            ChangeState(StateIds.LevelFail);
        }

        public void OnBtnLevelSuccessClicked()
        {
            if (CurrentState != StateIds.LevelSuccess)
                return;

            ChangeState(StateIds.StartNextLevel);
        }

        public void OnBtnLevelFailClicked() 
        {
            if (CurrentState != StateIds.LevelFail)
                return;

            ChangeState(StateIds.RestartLevel);
        }

        private void InitStateMachine() 
        {
            stateMachine = gameObject.AddComponent<StateMachine>();
            stateFactory = new StateFactory();

            stateMachine.InitStateMachine(stateInfo, stateFactory.GetStates(stateMachine));

            stateMachine.OnStateChanged += OnGameStateChanged;

            ChangeState(StateIds.None);
        }

        private void ChangeState(StateIds newState) 
        {
            stateMachine.ChangeState(newState);
        }

        private void OnGameStateChanged(StateIds newState) 
        {
            UIController.Instance?.OnGameStateChanged(newState);
        }

        private void LoadLevel() 
        {
            ChangeState(StateIds.LevelLoad);
        }

        private void StartLevel() 
        {
            ChangeState(StateIds.TapToStart);
        }
    }
}