using Cinemachine;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's X co-ordinate
    /// </summary>
    /// 
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraX : CinemachineExtension
    {
        public float posX = 0f;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam
            , CinemachineCore.Stage stage
            , ref CameraState state
            , float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                Vector3 pos = state.RawPosition;
                pos.x = posX;
                state.RawPosition = pos;
            }
        }
    }
}