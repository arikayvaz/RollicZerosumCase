using UnityEngine;
using Common;
using Gameplay.GameManagerStateMachine;

namespace Gameplay
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private StateInfo _stateInfo = new StateInfo();
        [SerializeField] private GameSettingsSO _gameSettings = null;


        private StateMachine _stateMachine;
        private StateFactory _stateFactory;

        public static GameSettingsSO GameSettings => Instance?._gameSettings;

        public StateIds CurrentState => _stateMachine?.State?.StateId ?? StateIds.None;
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
            StartGame();
        }

        private void InitStateMachine() 
        {
            _stateMachine = gameObject.AddComponent<StateMachine>();
            _stateFactory = new StateFactory();

            _stateMachine.InitStateMachine(_stateInfo, _stateFactory.GetStates(_stateMachine));

            _stateMachine.ChangeState(StateIds.None);
        }

        private void LoadLevel() 
        {
            _stateMachine.ChangeState(StateIds.LevelLoad);
        }

        private void StartGame() 
        {
            _stateMachine.ChangeState(StateIds.TapToStart);
        }
    }
}