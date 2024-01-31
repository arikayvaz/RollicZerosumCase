using UnityEngine;
using Common;
using Gameplay.PlayerStateMachine;
using Gameplay;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] StateInfo _stateInfo = new StateInfo();

    private StateMachine _stateMachine;
    private StateFactory _stateFactory;

    protected override void Awake()
    {
        base.Awake();

        InitStateMachine();
    }

    private void InitStateMachine()
    {
        _stateMachine = gameObject.AddComponent<StateMachine>();
        _stateFactory = new StateFactory();

        _stateMachine.InitStateMachine(_stateInfo, _stateFactory.GetStates(_stateMachine));

        _stateMachine.ChangeState(StateIds.None);
    }

    public void InitPlayer() 
    {
        _stateMachine.ChangeState(StateIds.Idle);
    }

    public void StartMoving() 
    {
        _stateMachine.ChangeState(StateIds.Move);
    }
}
