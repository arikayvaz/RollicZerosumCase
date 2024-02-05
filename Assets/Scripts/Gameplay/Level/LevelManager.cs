using UnityEngine;
using Common;
using User;
using Newtonsoft.Json;
using UnityEditor;
using Boo.Lang;

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
        [SerializeField] LevelGatePointController gatePointController = null;
        [SerializeField] LevelCollectibleItemController collectibleItemController = null;

        public LevelSaveModel CurrentSaveModel { get; private set; } = null;
        public bool IsLevelLoaded => CurrentSaveModel != null;

        public bool HasAnyCollectibleInLevel => collectibleItemController.HasAnyCollectible;

        public static void SetEditorInstance(LevelManager instance)
        {
            if (Application.isPlaying)
                return;

            Instance = instance;
        }

        public void OnLevelLoad() 
        {
            SetTotalLevelCount();
            int levelNo = GetLevelNo();
            LoadLevel(levelNo);
        }

        public void LoadLevel(int levelNo) 
        {
            CurrentSaveModel = GetLevelSaveModelFromResources(levelNo);

            if (CurrentSaveModel == null)
            {
                Debug.LogError("Level is not exist!");
                return;
            }

            InitializeLevelInfos();

            platformController.OnLevelLoad(CurrentSaveModel.platformSaveModel);
            gatePointController.OnLevelLoad(CurrentSaveModel.gatePointSaveModel);
            collectibleItemController.OnLevelLoad(CurrentSaveModel.collectibleItemSaveModel);
        }

        public void UnloadLevel() 
        {
            platformController.UnloadLevel();
            gatePointController.UnloadLevel();
            collectibleItemController.UnloadLevel();
        }

        public LevelSaveModel GetLevelSaveModelFromResources(int levelNo, bool isCreateLevelCheck = false) 
        {
            LevelSaveModelSO saveModelSO = (LevelSaveModelSO)Resources.Load(GetLevelDataPath(levelNo));

            if (saveModelSO == null)
            {
                if (!isCreateLevelCheck)
                    Debug.LogError("Level resource is missing for level: " + levelNo);

                return null;
            }

            saveModelSO = Instantiate(saveModelSO);
            saveModelSO.saveModel = JsonConvert.DeserializeObject<LevelSaveModel>(saveModelSO.modelJson);

            return saveModelSO.saveModel;
        }

        public string SetChangesAndGetSaveModelJson() 
        {
            //Platform
            CurrentSaveModel.platformSaveModel.infosJson = LevelDesigner.Instance.GetPlatformInfoJsonFromScene();
            CurrentSaveModel.platformSaveModelJson = JsonConvert.SerializeObject(CurrentSaveModel.platformSaveModel);

            //GatePoint
            CurrentSaveModel.gatePointSaveModel.infosJson = LevelDesigner.Instance.GetGatePointInfoJsonFromScene();
            CurrentSaveModel.gatePointSaveModelJson = JsonConvert.SerializeObject(CurrentSaveModel.gatePointSaveModel);

            //Collectible items
            CurrentSaveModel.collectibleItemSaveModel.infosJson = LevelDesigner.Instance.GetCollectibleItemInfoJsonFromScene();
            CurrentSaveModel.collectibleItemSaveModelJson = JsonConvert.SerializeObject(CurrentSaveModel.collectibleItemSaveModel);

            string modelString = JsonConvert.SerializeObject(CurrentSaveModel
                , Formatting.None
                , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return modelString == null ? "" : modelString;
        }

        public void InitializeLevelInfos() 
        {
            if (CurrentSaveModel.platformSaveModelJson == null)
                CurrentSaveModel.platformSaveModelJson = "";

            CurrentSaveModel.platformSaveModel = JsonConvert.DeserializeObject<PlatformLevelSaveModel>(CurrentSaveModel.platformSaveModelJson);

            if (CurrentSaveModel.platformSaveModel == null)
                CurrentSaveModel.platformSaveModel = new PlatformLevelSaveModel();

            if (CurrentSaveModel.gatePointSaveModelJson == null)
                CurrentSaveModel.gatePointSaveModelJson = "";

            CurrentSaveModel.gatePointSaveModel = JsonConvert.DeserializeObject<GatePointLevelSaveModel>(CurrentSaveModel.gatePointSaveModelJson);

            if (CurrentSaveModel.gatePointSaveModel == null)
                CurrentSaveModel.gatePointSaveModel = new GatePointLevelSaveModel();

            if (CurrentSaveModel.collectibleItemSaveModelJson == null)
                CurrentSaveModel.collectibleItemSaveModelJson = "";

            CurrentSaveModel.collectibleItemSaveModel = JsonConvert.DeserializeObject<CollectibleItemLevelSaveModel>(CurrentSaveModel.collectibleItemSaveModelJson);

            if (CurrentSaveModel.collectibleItemSaveModel == null)
                CurrentSaveModel.collectibleItemSaveModel = new CollectibleItemLevelSaveModel();
        }

        public string GetLevelSaveSOFileName(int levelNo) 
        {
            return LEVEL_FILE_ROOTH_PATH + GetLevelDataPath(levelNo) + ".asset";
        }

        #region Platform

        public void AddPlatform(PlatformLevelInfoModel infoModel) 
        {
            platformController.AddItem(infoModel);
        }

        public void RemoveLastPlatform() 
        {
            platformController.RemoveLastItem();
        }

        #endregion

        #region Gate Point

        public void AddGatePoint(GatePointLevelInfoModel infoModel) 
        {
            gatePointController.AddItem(infoModel);
        }

        public void RemoveLastGatePoint() 
        {
            gatePointController.RemoveLastItem();
        }

        #endregion

        #region CollectibleItem

        public void AddCollectibleItem(CollectibleItemLevelInfoModel infoModel) 
        {
            collectibleItemController.AddItem(infoModel);
        }

        public void RemoveCollectibleItem() 
        {
            collectibleItemController.RemoveLastItem();
        }

        public Vector3 GetNextCollectibleItemPosition() 
        {
            return collectibleItemController.GetNextCollectibleItemPosition();
        }

        #endregion

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

        private void SetTotalLevelCount() 
        {
            totalLevelCount = AssetDatabase.FindAssets($"t:{typeof(LevelSaveModelSO)}").Length;
        }

    }
}