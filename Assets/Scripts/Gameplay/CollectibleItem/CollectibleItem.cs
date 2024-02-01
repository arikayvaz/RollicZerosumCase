using UnityEngine;

namespace Gameplay
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] MeshRenderer renderer = null;
        [SerializeField] Rigidbody _rb = null;

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

        const float FORCE_POWER = 450f;
        public void Push() 
        {
            Vector3 force = Vector3.forward * FORCE_POWER * _rb.mass;

            _rb.AddForce(force);
        }
    }
}