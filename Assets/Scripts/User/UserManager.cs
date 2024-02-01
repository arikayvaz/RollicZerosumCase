using Common;

namespace User
{
    public class UserManager : Singleton<UserManager>
    {
        private UserSaveManager saveManager;

        public int UserLevelNo => saveManager.UserLevelNo;

        protected override void Awake()
        {
            base.Awake();

            saveManager = new UserSaveManager();
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