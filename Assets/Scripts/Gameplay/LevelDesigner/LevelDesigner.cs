﻿using Boo.Lang;
using Common;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class LevelDesigner : Singleton<LevelDesigner>
    {

#if UNITY_EDITOR
        private static bool isDesignMode = false;
        public static bool IsDesignMode
        {
            get
            {
                return Application.isPlaying ? false : isDesignMode;
            }
            set
            {
                isDesignMode = value;
            }
        }
#else
    public static bool IsDesignMode => false;
#endif

        [SerializeField] Transform trPlatformIndicator = null;

        [HideInInspector] public int levelNo = 0;
        [HideInInspector] public int gatePointTargetValue = 0;

        public float LastPlatformZ { get; private set; } = 0f;

        public const float PLATFORM_LENGTH = 20f;
        public const float GATE_POINT_LENGTH = 8f;

        public static void SetEditorInstance(LevelDesigner instance) 
        {
            if (Application.isPlaying)
                return;

            Instance = instance;
        }

        public string GetPlatformInfoJsonFromScene() 
        {
            GameObject[] platforms = GetLevelPlatforms();

            if (platforms == null || platforms.Length < 1)
                return "";

            PlatformLevelInfoModel[] infos = new PlatformLevelInfoModel[platforms.Length];

            for (int i = 0; i < infos.Length; i++)
            {
                PlatformLevelInfoModel info = new PlatformLevelInfoModel();
                info.z = platforms[i].transform.position.z;

                infos[i] = info;
            }

            string json = JsonConvert.SerializeObject(infos);

            return json == null ? "" : json;
        }

        public string GetGatePointInfoJsonFromScene() 
        {
            GatePointController[] gatePoints = GetLevelGatePoints();

            if (gatePoints == null || gatePoints.Length < 1)
                return "";

            GatePointLevelInfoModel[] infos = new GatePointLevelInfoModel[gatePoints.Length];

            for (int i = 0; i < gatePoints.Length; i++)
            {
                GatePointLevelInfoModel info = gatePoints[i].GetLevelInfoModel();
                infos[i] = info;
            }

            string json = JsonConvert.SerializeObject(infos);

            return json == null ? "" : json;
        }

        public void CalculateLastPlatformPosition() 
        {
            GameObject[] platforms = GetLevelPlatforms();
            GameObject[] gatePoints = GetLevelGatePointGameObjects();

            GameObject[] totalObjects = null;

            if ((platforms == null || platforms.Length < 1) && (gatePoints == null || gatePoints.Length < 1)) 
            {
                UpdateLastPlatformPosition(0f);
                return;
            }

            if (platforms == null || platforms.Length < 1)
                totalObjects = gatePoints;
            else if (gatePoints == null || gatePoints.Length < 1)
                totalObjects = platforms;
            else
                totalObjects = platforms.Concat(gatePoints).ToArray();

            totalObjects = totalObjects.OrderBy(x => x.transform.position.z).ToArray();

            GameObject goLastObject = totalObjects[totalObjects.Length - 1];

            LastPlatformZ = goLastObject.transform.position.z;

            bool isLastObjectPlatform = goLastObject.CompareTag("Platform");

            UpdateLastPlatformPosition(isLastObjectPlatform ? PLATFORM_LENGTH : GATE_POINT_LENGTH);
        }

        public void UpdateLastPlatformPosition(float delta) 
        {
            LastPlatformZ += delta;
        }

        public void UpdatePlatformIndicatorPosition() 
        {
            trPlatformIndicator.transform.position = new Vector3(0f, 0f, LastPlatformZ);
        }

        public void ResetPlatformPositionIndicator() 
        {
            LastPlatformZ = 0f;
            UpdatePlatformIndicatorPosition();
        }

        private GameObject[] GetLevelPlatforms() 
        {
            return GameObject.FindGameObjectsWithTag("Platform");
        }

        private GatePointController[] GetLevelGatePoints() 
        {
            return FindObjectsOfType<GatePointController>();
        }

        private GameObject[] GetLevelGatePointGameObjects() 
        {
            GatePointController[] gatePoints = GetLevelGatePoints();

            if (gatePoints == null || gatePoints.Length < 1)
                return null;

            GameObject[] goGatePoints = new GameObject[gatePoints.Length];

            for (int i = 0; i < goGatePoints.Length; i++)
            {
                goGatePoints[i] = gatePoints[i].gameObject;
            }

            return goGatePoints;
        }

    }
}