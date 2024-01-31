using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Player Settings", menuName = "Project/Gameplay/Player Settings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        public float playerHalfSize = 1.75f;
        public float moveSpeed = 5f;
    }
}