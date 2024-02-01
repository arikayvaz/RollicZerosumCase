using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = LevelManager.LEVEL_FILE_NAME, menuName = "Project/Gameplay/Level/New Level File")]
    public class LevelDataSO : ScriptableObject
    {
        public string modelString = "";
        //[System.NonSerialized]
        public LevelDataModel dataModel;
    }
}