using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    [SelectionBase]
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] MeshRenderer renderer = null;
        [SerializeField] Rigidbody _rb = null;

        [SerializeField] CollectibleItemTypes itemType = CollectibleItemTypes.None;

        public CollectibleItemLevelInfoModel GetInfoModel() 
        {
            return new CollectibleItemLevelInfoModel(itemType, transform.position.x, transform.position.z);
        }

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

        const float FORCE_POWER = 60f;
        public void Push() 
        {
            //_rb.DOMoveZ(transform.position.z + 4f, 1f);

            Vector3 force = Vector3.forward * FORCE_POWER;

            _rb.AddForce(force);
        }
    }
}