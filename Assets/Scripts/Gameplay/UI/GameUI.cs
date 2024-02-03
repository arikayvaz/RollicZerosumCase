using TMPro;
using UnityEngine;
using User;

namespace Gameplay
{
    public class GameUI : UIBase
    {
        [SerializeField] TMP_Text textCurrentLevel = null;

        public override void InitUI()
        {
            textCurrentLevel.text = $"Level {UserManager.Instance.UserLevelNo + 1}";
        }
    }
}