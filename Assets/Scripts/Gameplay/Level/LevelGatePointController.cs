using Boo.Lang;
using Common;
using System;
using UnityEngine;

namespace Gameplay
{
    public class LevelGatePointController : MonoBehaviour, ILevelController<GatePointLevelSaveModel, GatePointLevelInfoModel>
    {
        [SerializeField] Pooler pooler;

        List<GatePointController> gatePoints = null;

        public void OnLevelLoad(GatePointLevelSaveModel saveModel)
        {
            gatePoints = new List<GatePointController>();

            SpawnItems(saveModel.GetLevelInfos());
        }

        public void UnloadLevel()
        {
            if (!Application.isPlaying && gatePoints?.Count > 0)
            {
                foreach (GatePointController gatePoint in gatePoints)
                {
                    DestroyImmediate(gatePoint.gameObject);
                }
            }

            gatePoints = null;
        }

        public void SpawnItems(GatePointLevelInfoModel[] levelInfos) 
        {
            if (levelInfos == null || levelInfos.Length < 1)
                return;

            for (int i = 0; i < levelInfos.Length; i++)
            {
                AddGatePoint(levelInfos[i], i == levelInfos.Length - 1);
            }
        }

        public void AddItem(GatePointLevelInfoModel levelInfo) 
        {
            AddGatePoint(levelInfo, false);
        }

        public void AddGatePoint(GatePointLevelInfoModel infoModel, bool isLastGate) 
        {
            GatePointController gatePoint = SpawnGatePoint(infoModel.Position, infoModel.targetValue, isLastGate);
            gatePoints.Add(gatePoint);
        }

        public void RemoveLastItem() 
        {
            if (Application.isPlaying || gatePoints?.Count < 1)
                return;

            GatePointController lastGatePoint = gatePoints[gatePoints.Count - 1];

            gatePoints.Remove(lastGatePoint);
            DestroyImmediate(lastGatePoint.gameObject);
        }

        public GatePointController SpawnGatePoint(Vector3 position, int targetValue, bool isLastGate) 
        {
            GatePointController gatePoint = pooler.GetGo<GatePointController>();

            gatePoint.gameObject.SetActive(true);
            gatePoint.transform.position = position;

            gatePoint.InitController(targetValue, isLastGate);

            return gatePoint;
        }
    }
}