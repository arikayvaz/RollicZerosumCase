using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCollectionController : MonoBehaviour
    {
        private List<CollectibleItem> collectedItems = null;

        public void InitController() 
        {
            collectedItems = new List<CollectibleItem>();
        }

        public void HandleTriggerEnter(GameObject goCollided) 
        {
            CollectibleItem item = GetCollectionItem(goCollided);

            if (item == null)
                return;

            AddItemToCollection(item);
        }

        public void HandleTriggerExit(GameObject goCollided)
        {
            CollectibleItem item = GetCollectionItem(goCollided);

            if (item == null)
                return;

            RemoveItemFromCollection(item);
        }

        private CollectibleItem GetCollectionItem(GameObject go) 
        {
            return go.GetComponent<CollectibleItem>();
        }

        private void AddItemToCollection(CollectibleItem item) 
        {
            if (collectedItems.Contains(item))
                return;

            collectedItems.Add(item);
            item.OnAdded();
            Debug.Log("Added Item");
        }

        private void RemoveItemFromCollection(CollectibleItem item) 
        {
            if (!collectedItems.Contains(item))
                return;

            collectedItems.Remove(item);
            item.OnRemoved();
            Debug.Log("Removed Item");
        }
    }
}