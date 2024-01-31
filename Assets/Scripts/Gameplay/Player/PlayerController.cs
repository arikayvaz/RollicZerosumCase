using UnityEngine;
using Common;
using Gameplay.PlayerStateMachine;
using Gameplay;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] StateInfo _stateInfo = new StateInfo();
    [SerializeField] PlayerCollectionController _collectionController = null;

    private StateMachine _stateMachine;
    private StateFactory _stateFactory;

    public bool CanCheckCollection => _stateMachine?.State?.StateId == StateIds.Move;

    protected override void Awake()
    {
        base.Awake();

        InitStateMachine();
        _collectionController.InitController();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanCheckCollection)
            _collectionController.HandleTriggerEnter(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (CanCheckCollection)
            _collectionController.HandleTriggerExit(other.gameObject);
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
