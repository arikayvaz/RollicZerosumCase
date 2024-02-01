using UnityEngine;
using Common;
using User;
using Newtonsoft.Json;
using UnityEditor;

namespace Gameplay
{
    public class LevelManager : Singleton<LevelManager>
    {
#if UNITY_EDITOR
        [SerializeField] bool debugMode = true;
        [SerializeField] int debugLevelNo = 0;
#else
    private bool debugMode = false;
    private int debugLevelNo = 0;
#endif

        public const string LEVEL_FILE_NAME = "Level_";
        public const string LEVEL_FILE_PATH = "Levels/";
        public const string LEVEL_FILE_ROOTH_PATH = "Assets/Resources/";

        public static bool IsDebugMode => Instance?.debugMode ?? false;
        public static int DebugLevelNo => Instance?.debugLevelNo ?? 0;

        private int totalLevelCount = 0;

        [SerializeField] LevelPlatformController platformController = null;
        public LevelPlatformController PlatformController => platformController;

        public LevelDataModel levelDataModel = null;

        public static void SetEditorInstance(LevelManager instance)
        {
            if (Application.isPlaying)
                return;

            Instance = instance;
        }

        public LevelDataModel GetLevelDataModelFromResources(int levelNo, bool isCreateLevelCheck = false) 
        {
            LevelDataSO levelData = (LevelDataSO)Resources.Load(GetLevelDataPath(levelNo));

            if (levelData == null)
            {
                if (!isCreateLevelCheck)
                    Debug.LogError("Level resource is missing for level: " + levelNo);

                return null;
            }

            levelData = Instantiate(levelData);
            levelData.dataModel = JsonConvert.DeserializeObject<LevelDataModel>(levelData.modelString);

            return levelData.dataModel;
        }

        public string SetChangesToDataModel() 
        {
            levelDataModel.platformLevelDataModel.platformInfosJson = LevelDesigner.Instance.GetPlatformInfoJsonFromScene();
            levelDataModel.platformLevelDataModelJson = JsonConvert.SerializeObject(levelDataModel.platformLevelDataModel);

            string modelString = JsonConvert.SerializeObject(levelDataModel
                , Formatting.None
                , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return modelString == null ? "" : modelString;
        }

        public void InitializeLevelInfos() 
        {
            if (levelDataModel.platformLevelDataModelJson == null)
                levelDataModel.platformLevelDataModelJson = "";

            levelDataModel.platformLevelDataModel = JsonConvert.DeserializeObject<PlatformLevelDataModel>(levelDataModel.platformLevelDataModelJson);

            if (levelDataModel.platformLevelDataModel == null)
                levelDataModel.platformLevelDataModel = new PlatformLevelDataModel();
        }

        public string GetLevelDataSOFileName(int levelNo) 
        {
            return LEVEL_FILE_ROOTH_PATH + GetLevelDataPath(levelNo) + ".asset";
        }

        private string GetLevelDataPath(int levelNo) 
        {
            return LEVEL_FILE_PATH + LEVEL_FILE_NAME + GetLevelString(levelNo);
        }

        private string GetLevelName() 
        {
            int levelNo = GetLevelNo();

            return LEVEL_FILE_NAME + GetLevelString(levelNo);
        }

        private string GetLevelString(int i) 
        {
            if (i < 10)
                return "000" + i.ToString();
            if (i < 100)
                return "00" + i.ToString();
            if (i < 1000)
                return "0" + i.ToString();

            return "" + i.ToString();
        }

        private int GetLevelNo() 
        {
            if (IsDebugMode)
                return Mathf.Min(DebugLevelNo, totalLevelCount - 1);

            int userLevelNo = UserManager.Instance.UserLevelNo;

            if (userLevelNo < totalLevelCount)
                return userLevelNo;

            return Random.Range(0, totalLevelCount);
        }

    }
}