using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Level Settings", menuName = "Project/Gameplay/LevelSettings")]
    public class LevelSettingsSO : ScriptableObject
    {
        //TODO: Update level objects from settings' values
        public float roadHalfWidth = 8f;
        public float platformLength = 20f;
        public float gatePointLength = 8f;
        public float collectibleItemSize = 0.3f;
    }
}