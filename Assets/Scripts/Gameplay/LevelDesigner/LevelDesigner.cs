using Boo.Lang;
using Common;
using Newtonsoft.Json;
using System;
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
        [SerializeField] LevelSettingsSO levelSettings = null;
        public LevelSettingsSO LevelSettings => levelSettings;

        [Space]
        [SerializeField] Transform trPlatformIndicator = null;
        [SerializeField] Transform trCollectibleIndicator = null;

        [HideInInspector] public int levelNo = 0;
        [HideInInspector][NonSerialized] public int gatePointTargetValue = 0;
        [HideInInspector][NonSerialized] public CollectibleItemTypes collectibleItemType = CollectibleItemTypes.None;
        [HideInInspector][NonSerialized] public int collectibleAddCount = 0;
        [HideInInspector][NonSerialized] public int collectibleDeleteCount = 0;

        public float LastPlatformZ { get; private set; } = 0f;

        public const float COLLECTIBLE_ITEM_GAP = 0.4f;

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

            infos = infos.OrderBy(x => x.z).ToArray();

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

            infos = infos.OrderBy(x => x.z).ToArray();

            string json = JsonConvert.SerializeObject(infos);

            return json == null ? "" : json;
        }

        public string GetCollectibleItemInfoJsonFromScene() 
        {
            CollectibleItem[] items = GetCollectibleItems();

            if (items == null || items.Length < 1)
                return "";

            CollectibleItemLevelInfoModel[] infos = new CollectibleItemLevelInfoModel[items.Length];

            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = items[i].GetInfoModel();
            }

            infos = infos.OrderBy(x => x.z).ToArray();

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

            UpdateLastPlatformPosition(isLastObjectPlatform ? levelSettings.platformLength : levelSettings.gatePointLength);
        }

        public void CalculateLastCollectiblePosition() 
        {
            GameObject[] goItems = GetLevelCollectibleItemGameObjects();

            if (goItems == null || goItems.Length < 1) 
            {
                SetCollectibItemIndicatorPosition(Vector3.zero);
                return;
            }

            goItems = goItems.OrderBy(x => x.transform.position.z).ToArray();

            Vector3 pos = goItems[goItems.Length - 1].transform.position;
            pos.z += levelSettings.collectibleItemSize * 2f + COLLECTIBLE_ITEM_GAP;

            SetCollectibItemIndicatorPosition(pos);
        }

        public void UpdateLastPlatformPosition(float delta) 
        {
            LastPlatformZ += delta;
        }

        public void UpdatePlatformIndicatorPosition() 
        {
            trPlatformIndicator.transform.position = new Vector3(0f, 0f, LastPlatformZ);
        }

        public void ResetPlatformIndicatorPosition() 
        {
            LastPlatformZ = 0f;
            UpdatePlatformIndicatorPosition();
        }

        public void SetCollectibItemIndicatorPosition(Vector3 position) 
        {
            trCollectibleIndicator.position = position;
        }

        public void UpdateCollectibleIndicatorPosition() 
        {
            trCollectibleIndicator.position = LevelManager.Instance.GetNextCollectibleItemPosition();
        }

        public Vector3 GetCollectibleIndicatorPosition() => trCollectibleIndicator.position;

        public void ResetCollectibleItemIndicatorPosition() 
        {
            trCollectibleIndicator.position = Vector3.zero;
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

        private CollectibleItem[] GetCollectibleItems()
        {
            return FindObjectsOfType<CollectibleItem>();
        }

        private GameObject[] GetLevelCollectibleItemGameObjects() 
        {
            CollectibleItem[] items = GetCollectibleItems();

            if (items == null || items.Length < 1)
                return null;

            GameObject[] goCollectibleItems = new GameObject[items.Length];

            for (int i = 0; i < goCollectibleItems.Length; i++)
            {
                goCollectibleItems[i] = items[i].gameObject;
            }

            return goCollectibleItems;
        }

    }
}