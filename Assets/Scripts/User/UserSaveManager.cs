using Gameplay;
using UnityEngine;

namespace User
{
    public class UserSaveManager
    {

        const string KEY_LOCAL_SAVE_STRING = "localSaveString";

        private UserSaveModel saveModel;

        public int UserLevelNo => saveModel.levelNo;
        public int LastPlayerLevelNo => saveModel.lastPlayedLevelNo;

        public UserSaveManager()
        {
            
        }

        public void IncreaseLevelNoAndSave() 
        {
            IncreaseLevelNo();
            Save();
        }

        public void LoadUserSave() 
        {
            Load();
        }

        public void DeleteAllUserSettings() 
        {
            ResetSave();
            PlayerPrefs.DeleteAll();
            Save();
        }

        public void ResetLastPlayedLevelNoAndSave() 
        {
            saveModel.lastPlayedLevelNo = -1;
            Save();
        }

        public void SetLastPlayedLevelNoAndSave(int levelNo)
        {
            if (levelNo == saveModel.lastPlayedLevelNo)
                return;

            saveModel.lastPlayedLevelNo = levelNo;
            Save();
        }

        private void Save() 
        {
            if (saveModel == null)
                saveModel = new UserSaveModel();

            string localSaveString = JsonUtility.ToJson(saveModel);

            if (localSaveString == null)
                localSaveString = "";

            PlayerPrefs.SetString(KEY_LOCAL_SAVE_STRING, localSaveString);
            PlayerPrefs.Save();
        }

        private void ResetSave() 
        {
            saveModel = new UserSaveModel();

            if (LevelManager.IsDebugMode)
                saveModel.levelNo = LevelManager.DebugLevelNo;
        }

        private void Load() 
        {
            string localSaveString = PlayerPrefs.GetString(KEY_LOCAL_SAVE_STRING, null);

            if (localSaveString == null)
                return;

            saveModel = JsonUtility.FromJson<UserSaveModel>(localSaveString);

            if (saveModel == null)
                saveModel = new UserSaveModel();

        }

        private void IncreaseLevelNo() 
        {
            saveModel.levelNo++;
        }
    }
}