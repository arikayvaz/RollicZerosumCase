using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace Gameplay
{
    [CustomEditor(typeof(LevelDesigner))]
    public class LevelDesignerEditor : Editor
    {
        LevelDesigner designer = null;

        LevelManager LevelMngr => LevelManager.Instance;

        private void OnEnable()
        {
            designer = target as LevelDesigner;

            if (LevelDesigner.Instance == null) 
            {
                LevelDesigner.SetEditorInstance(designer);
            }

            if (LevelManager.Instance == null)
            {
                LevelManager lm = FindObjectOfType<LevelManager>();
                LevelManager.SetEditorInstance(lm);
            }
        }

        public override void OnInspectorGUI()
        {
            DrawCreateAndLoad();
            DrawPlatform();
            DrawSaveAndUnload();

            DrawDefaultInspector();
        }

        private void DrawCreateAndLoad() 
        {
            int levelNo = EditorGUILayout.IntField("Level No:", designer.levelNo);

            if (levelNo != designer.levelNo)
            {
                designer.levelNo = levelNo;
                EditorUtility.SetDirty(designer);
            }

            if (GUILayout.Button("Load Level"))
            {
                LevelDesigner.IsDesignMode = true;
                LoadLevel();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Level"))
            {
                LevelDesigner.IsDesignMode = true;
                CreateLevel();
            }
        }

        private void LoadLevel() 
        {
            LevelMngr.LoadLevel(designer.levelNo);
        }

        private void CreateLevel() 
        {
            if (LevelMngr == null)
                return;

            LevelSaveModel levelData = LevelMngr.GetLevelSaveModelFromResources(designer.levelNo, true);

            if (levelData != null)
            {
                Debug.LogError("Level already exists: " + designer.levelNo);
                return;
            }

            LevelSaveModelSO levelDataSO = CreateInstance<LevelSaveModelSO>();
            levelDataSO.saveModel = new LevelSaveModel();
            levelDataSO.modelJson = JsonConvert.SerializeObject(levelDataSO.saveModel
                , Formatting.None
                , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

            AssetDatabase.CreateAsset(levelDataSO, LevelMngr.GetLevelSaveSOFileName(designer.levelNo));
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(designer);
            EditorUtility.SetDirty(LevelMngr);

            Debug.Log("Level Created");
        }

        private void DrawSaveAndUnload() 
        {
            if (!LevelDesigner.IsDesignMode)
                return;

            if (GUILayout.Button("Unload Level"))
            {
                UnloadLevel();
                LevelDesigner.IsDesignMode = false;
            }

            if (GUILayout.Button("Save Level"))
            {
                SaveLevel(true);
                LevelDesigner.IsDesignMode = false;
            }
        }

        private void SaveLevel(bool unloadLevel) 
        {
            AssetDatabase.Refresh();

            //LevelMngr.SetChangesToDataModel();
            //LevelDataSO levelData =  LevelMngr.SetChangesToLevelDataSO(designer.levelNo);

            //AssetDatabase.CreateAsset(levelData, LevelMngr.GetLevelDataSOFileName(designer.levelNo));
            //AssetDatabase.SaveAssets();

            LevelSaveModelSO levelDataSO = CreateInstance<LevelSaveModelSO>();

            string modelJson = LevelMngr.SetChangesAndGetSaveModelJson();

            levelDataSO.modelJson = modelJson;

            AssetDatabase.CreateAsset(levelDataSO, LevelMngr.GetLevelSaveSOFileName(designer.levelNo));
            AssetDatabase.SaveAssets();

            if (unloadLevel)
                UnloadLevel();

        }

        private void UnloadLevel() 
        {
            LevelMngr.PlatformController.UnloadLevel();

            designer.lastPlatformZ = 0f;
        }

        private void DrawPlatform() 
        {
            if (!LevelDesigner.IsDesignMode)
                return;

            if (GUILayout.Button("Add Platform")) 
            {
                AddPlatform();
            }
        }

        private void AddPlatform() 
        {
            const float PLATFORM_LENGTH = 20f;

            PlatformLevelInfoModel infoModel = new PlatformLevelInfoModel();
            infoModel.z = designer.lastPlatformZ;

            LevelMngr.PlatformController.AddPlatform(infoModel);

            designer.lastPlatformZ += PLATFORM_LENGTH;
        }
    }
}