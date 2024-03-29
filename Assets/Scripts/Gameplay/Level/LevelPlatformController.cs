﻿using Boo.Lang;
using Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class LevelPlatformController : MonoBehaviour, ILevelController<PlatformLevelSaveModel, PlatformLevelInfoModel>
    {
        [SerializeField] Pooler pooler;

        List<GameObject> platforms = null;

        public void OnLevelLoad(PlatformLevelSaveModel saveModel) 
        {
            platforms = new List<GameObject>();

            SpawnItems(saveModel.GetLevelInfos());
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

        public void SpawnItems(PlatformLevelInfoModel[] levelInfos) 
        {
            if (levelInfos == null || levelInfos.Length < 1)
                return;

            foreach (PlatformLevelInfoModel info in levelInfos)
            {
                AddItem(info);
            }
        }

        public void AddItem(PlatformLevelInfoModel infoModel) 
        {
            GameObject platform = SpawnPlatform(infoModel.Position);
            platforms.Add(platform);
        }

        public bool RemoveLastItem() 
        {
            if (Application.isPlaying || platforms?.Count < 1)
                return false;

            GameObject goLastPlatform = platforms[platforms.Count - 1];

            platforms.Remove(goLastPlatform);
            DestroyImmediate(goLastPlatform);

            return true;
        }

        private GameObject SpawnPlatform(Vector3 position) 
        {
            GameObject platform = pooler.GetGo(null);

            platform.gameObject.SetActive(true);
            platform.transform.position = position;

            return platform;
        }
    }
}