using Boo.Lang;
using Common;
using Newtonsoft.Json;
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

        [HideInInspector] public int levelNo = 0;
        public float lastPlatformZ = 0f;

        public static void SetEditorInstance(LevelDesigner instance) 
        {
            if (Application.isPlaying)
                return;

            Instance = instance;
        }

        public string GetPlatformInfoJsonFromScene() 
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

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

    }
}