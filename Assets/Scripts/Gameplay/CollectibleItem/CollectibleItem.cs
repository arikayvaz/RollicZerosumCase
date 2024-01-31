using UnityEngine;

namespace Gameplay
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] MeshRenderer renderer = null;

        public void OnAdded() 
        {
            Material mat = renderer.material;
            mat.color = Color.green;
        }

        public void OnRemoved() 
        {
            Material mat = renderer.material;
            mat.color = Color.red;
        }
    }
}