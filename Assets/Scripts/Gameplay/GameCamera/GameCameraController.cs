using Cinemachine;
using Common;
using UnityEngine;

namespace Gameplay
{
    public class GameCameraController : Singleton<GameCameraController>
    {
        [SerializeField] CinemachineVirtualCamera playerFollowCamera = null;

        public void InitController(Transform trTarget) 
        {
            playerFollowCamera.m_Follow = trTarget;
            playerFollowCamera.m_LookAt = trTarget;
        }
    }
}