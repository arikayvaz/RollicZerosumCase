using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = LevelManager.LEVEL_FILE_NAME, menuName = "Project/Gameplay/Level/New Level File")]
    public class LevelSaveModelSO : ScriptableObject
    {
        public string modelJson = "";
        //[System.NonSerialized]
        public LevelSaveModel saveModel;
    }
}