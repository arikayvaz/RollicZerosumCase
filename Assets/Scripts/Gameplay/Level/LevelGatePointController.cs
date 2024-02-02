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

            foreach (GatePointLevelInfoModel info in levelInfos)
            {
                AddItem(info);
            }
        }

        public void AddItem(GatePointLevelInfoModel infoModel) 
        {
            GatePointController gatePoint = SpawnGatePoint(infoModel.Position, infoModel.targetValue);
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

        public GatePointController SpawnGatePoint(Vector3 position, int targetValue) 
        {
            GatePointController gatePoint = pooler.GetGo<GatePointController>(pooler.transform);

            gatePoint.gameObject.SetActive(true);
            gatePoint.transform.position = position;

            gatePoint.InitController(targetValue);

            return gatePoint;
        }
    }
}