using UnityEngine;

namespace Gameplay
{
    public class LevelSuccessUI : UIBase
    {
        public override void InitUI() { }

        public void OnBtnLevelSuccessClick() 
        {
            GameManager.Instance.OnBtnLevelSuccessClicked();
        }
    }
}