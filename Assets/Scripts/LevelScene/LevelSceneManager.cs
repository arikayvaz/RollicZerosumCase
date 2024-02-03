using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelScene
{
    public class LevelSceneManager : MonoBehaviour
    {
        const string SCENE_NAME_GAMEPLAY = "Gameplay";

        public static void LoadScene() 
        {
            SceneManager.LoadScene(SCENE_NAME_GAMEPLAY);
        }
    }
}