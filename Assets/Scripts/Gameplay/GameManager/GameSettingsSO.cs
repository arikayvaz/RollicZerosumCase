using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Project/Gameplay/GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        public float roadHalfWidth = 8f;
    }
}