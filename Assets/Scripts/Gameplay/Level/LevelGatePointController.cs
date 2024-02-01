using Common;
using System;
using UnityEngine;

namespace Gameplay
{
    public class LevelGatePointController : MonoBehaviour, ILevelController<GatePointLevelSaveModel>
    {
        [SerializeField] Pooler pooler;

        public void OnLevelLoad(GatePointLevelSaveModel saveModel)
        {

        }

        public void UnloadLevel()
        {

        }
    }
}