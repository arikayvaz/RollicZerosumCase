using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Level Settings", menuName = "Project/Gameplay/LevelSettings")]
    public class LevelSettingsSO : ScriptableObject
    {
        public int totalLevelCount = 0;

        //TODO: Update level objects from settings' values
        [Space]
        public float roadHalfWidth = 8f;
        public float platformLength = 20f;
        public float gatePointLength = 8f;
        public float collectibleItemSize = 0.3f;
    }
}