using Boo.Lang;
using Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class LevelPlatformController : MonoBehaviour, ILevelController<PlatformLevelSaveModel>
    {
        [SerializeField] Pooler pooler;

        List<GameObject> platforms = null;

        public void OnLevelLoad(PlatformLevelSaveModel saveModel) 
        {
            platforms = new List<GameObject>();

            SpawnPlatforms(saveModel.GetLevelInfos());
        }

        public void UnloadLevel() 
        {
            if (!Application.isPlaying && platforms?.Count > 0)
            {
                foreach (GameObject platform in platforms)
                {
                    DestroyImmediate(platform);
                }
            }

            platforms = null;
        }

        public void SpawnPlatforms(PlatformLevelInfoModel[] levelInfos) 
        {
            if (levelInfos == null || levelInfos.Length < 1)
                return;

            foreach (PlatformLevelInfoModel info in levelInfos)
            {
                AddPlatform(info);
            }
        }

        public void AddPlatform(PlatformLevelInfoModel infoModel) 
        {
            GameObject platform = SpawnPlatform(infoModel.Position);
            platforms.Add(platform);
        }

        private GameObject SpawnPlatform(Vector3 position) 
        {
            GameObject platform = pooler.GetGo(pooler.transform);

            platform.gameObject.SetActive(true);
            platform.transform.position = position;

            return platform;
        }
    }
}