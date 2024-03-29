﻿using Common.GenericStateMachine;
using UnityEngine;

namespace Gameplay.PlayerStateMachine
{
    [System.Serializable]
    public class StateInfo : GenericStateInfo
    {
        [SerializeField] private PlayerSettingsSO _playerSettings = null;
        public PlayerCollectionController collectionController = null;

        public Rigidbody rb = null;
        public float PlayerHalfSize => _playerSettings.playerHalfSize;
        public float MoveSpeed => _playerSettings.moveSpeed;

        [HideInInspector] public GatePointController currentGatePoint = null;
    }
}