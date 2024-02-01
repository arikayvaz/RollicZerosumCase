﻿using Gameplay;
using UnityEngine;

namespace User
{
    public class UserSaveManager
    {

        const string KEY_LOCAL_SAVE_STRING = "localSaveString";

        private UserSaveModel saveModel;

        public int UserLevelNo => saveModel.levelNo;

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
            Reset();
            PlayerPrefs.DeleteAll();
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

        private void Reset() 
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