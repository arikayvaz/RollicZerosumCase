using UnityEngine;
using UnityEditor;

namespace User
{
    [CustomEditor(typeof(UserManager))]
    public class UserManagerEditor : Editor
    {
        UserManager manager;

        private void OnEnable()
        {
            manager = target as UserManager;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Delete User Save"))
            {
                if (!manager.IsInitialized)
                    manager.InitManager();

                manager.DeleteAllUserSettings();
                Debug.Log("User save deleted");
                return;
            }

            EditorGUILayout.Space(10f);

            DrawDefaultInspector();
        }
    }
}