using UnityEngine;

namespace Gameplay
{
    public class LevelFailUI : UIBase
    {
        public override void InitUI() { }

        public void OnBtnLevelFailClick()
        {
            GameManager.Instance.OnBtnLevelFailClicked();
        }
    }
}