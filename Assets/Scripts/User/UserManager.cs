using Common;

namespace User
{
    public class UserManager : Singleton<UserManager>
    {
        private UserSaveManager saveManager;

        public bool IsInitialized { get; private set; } = false;

        public int UserLevelNo => saveManager.UserLevelNo;

        public void InitManager() 
        {
            saveManager = new UserSaveManager();

            IsInitialized = true;
        }

        public void IncreaseLevelNoAndSave() 
        {
            saveManager.IncreaseLevelNoAndSave();
        }

        public void LoadUserSave() 
        {
            saveManager.LoadUserSave();
        }

        public void DeleteAllUserSettings() 
        {
            saveManager.DeleteAllUserSettings();
        }
    }
}