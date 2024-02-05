using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    [SelectionBase]
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] Rigidbody _rb = null;

        [SerializeField] CollectibleItemTypes itemType = CollectibleItemTypes.None;

        public CollectibleItemLevelInfoModel GetInfoModel() 
        {
            return new CollectibleItemLevelInfoModel(itemType, transform.position, transform.rotation.eulerAngles);
        }

        const float FORCE_POWER = 60f;
        public void Push() 
        {
            _rb.DOMoveZ(transform.position.z + 8f, 1f);

            /*
            Vector3 force = Vector3.forward * FORCE_POWER;

            _rb.AddForce(force);
            */
        }
    }
}