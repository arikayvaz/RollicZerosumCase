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

            EditorGUILayout.Space(10f);

            DrawPlatform();

            EditorGUILayout.Space(10f);

            DrawGatePoint();

            EditorGUILayout.Space(10f);

            DrawCollectibleItem();

            EditorGUILayout.Space(10f);

            DrawSaveAndUnload();

            EditorGUILayout.Space(10f);

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

            designer.CalculateLastPlatformPosition();
            designer.UpdatePlatformIndicatorPosition();
            //designer.UpdateCollectibleIndicatorPosition();
            designer.CalculateLastCollectiblePosition();
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
            LevelMngr.UnloadLevel();

            designer.ResetPlatformIndicatorPosition();
            designer.ResetCollectibleItemIndicatorPosition();
        }

        #region Platform

        private void DrawPlatform() 
        {
            if (!LevelDesigner.IsDesignMode)
                return;

            GUILayout.Label("Platform");

            EditorGUILayout.Space(5f);

            if (GUILayout.Button("Add Platform")) 
            {
                AddPlatform();
                return;
            }

            if (GUILayout.Button("Delete Last Platform"))
            {
                DeleteLastPlatform();
                return;
            }
        }

        private void AddPlatform() 
        {
            PlatformLevelInfoModel infoModel = new PlatformLevelInfoModel();
            infoModel.z = designer.LastPlatformZ;

            LevelMngr.AddPlatform(infoModel);

            designer.UpdateLastPlatformPosition(LevelDesigner.PLATFORM_LENGTH);
            designer.UpdatePlatformIndicatorPosition();
        }

        private void DeleteLastPlatform() 
        {
            LevelMngr.RemoveLastPlatform();

            designer.UpdateLastPlatformPosition(-LevelDesigner.PLATFORM_LENGTH);
            designer.UpdatePlatformIndicatorPosition();
        }

        #endregion

        #region Gate Point

        private void DrawGatePoint() 
        {
            if (!LevelDesigner.IsDesignMode)
                return;

            GUILayout.Label("Platform");

            EditorGUILayout.Space(5f);

            int targetValue = EditorGUILayout.IntField("Target Value:", designer.gatePointTargetValue);

            if (targetValue != designer.gatePointTargetValue)
            {
                designer.gatePointTargetValue = targetValue;
                EditorUtility.SetDirty(designer);
            }

            if (GUILayout.Button("Add Gate Point"))
            {
                AddGatePoint();
                return;
            }

            if (GUILayout.Button("Delete Last Gate Point"))
            {
                DeleteLastGatePoint();
                return;
            }
        }

        private void AddGatePoint() 
        {
            GatePointLevelInfoModel infoModel = new GatePointLevelInfoModel();
            infoModel.z = designer.LastPlatformZ;
            infoModel.targetValue = designer.gatePointTargetValue;

            LevelMngr.AddGatePoint(infoModel);

            designer.UpdateLastPlatformPosition(LevelDesigner.GATE_POINT_LENGTH);
            designer.UpdatePlatformIndicatorPosition();
        }

        private void DeleteLastGatePoint() 
        {
            LevelMngr.RemoveLastGatePoint();

            designer.UpdateLastPlatformPosition(-LevelDesigner.GATE_POINT_LENGTH);
            designer.UpdatePlatformIndicatorPosition();
        }

        #endregion

        #region CollectibleItem

        private void DrawCollectibleItem() 
        {
            if (!LevelDesigner.IsDesignMode)
                return;

            GUILayout.Label("Collectible Item");

            EditorGUILayout.Space(5f);

            CollectibleItemTypes itemType = (CollectibleItemTypes) EditorGUILayout.EnumPopup("Collectible Type:", designer.collectibleItemType);

            if (designer.collectibleItemType != itemType)
            {
                designer.collectibleItemType = itemType;
                EditorUtility.SetDirty(designer);
            }

            int addCount = EditorGUILayout.IntField("Add Count:", designer.collectibleAddCount);

            if (designer.collectibleAddCount != addCount)
            {
                designer.collectibleAddCount = addCount;
                EditorUtility.SetDirty(designer);
            }

            int deleteCount = EditorGUILayout.IntField("Add Count:", designer.collectibleDeleteCount);

            if (designer.collectibleDeleteCount != deleteCount)
            {
                designer.collectibleDeleteCount = deleteCount;
                EditorUtility.SetDirty(designer);
            }

            if (GUILayout.Button("Add Single"))
            {
                AddSingleCollectible();
                return;
            }

            if (GUILayout.Button("Add Multiple"))
            {
                AddMultipleCollectible(addCount);
                return;
            }

            if (GUILayout.Button("Delete Single"))
            {
                DeleteSingleCollectible();
                return;
            }

            if (GUILayout.Button("Delete Multiple"))
            {
                DeleteMultipleCollectible(deleteCount);
                return;
            }
        }

        private void AddSingleCollectible() 
        {
            CollectibleItemLevelInfoModel infoModel = new CollectibleItemLevelInfoModel();

            infoModel.type = designer.collectibleItemType;

            Vector3 pos = designer.GetCollectibleIndicatorPosition();
            infoModel.x = pos.x;
            infoModel.z = pos.z;

            LevelMngr.AddCollectibleItem(infoModel);

            designer.UpdateCollectibleIndicatorPosition();
        }

        private void AddMultipleCollectible(int count) 
        {
            for (int i = 0; i < count; i++)
                AddSingleCollectible();
        }

        private void DeleteSingleCollectible() 
        {
            LevelMngr.RemoveCollectibleItem();

            designer.UpdateCollectibleIndicatorPosition();
        }

        private void DeleteMultipleCollectible(int count) 
        {
            for (int i = 0; i < count; i++)
                DeleteSingleCollectible();
        }

        #endregion
    }
}