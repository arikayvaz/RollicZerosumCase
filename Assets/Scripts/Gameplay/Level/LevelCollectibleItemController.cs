using Boo.Lang;
using Common;
using System;
using UnityEngine;

namespace Gameplay
{
    public enum CollectibleItemTypes { None = -1, Type1, Type2, Type3 }

    public class LevelCollectibleItemController : MonoBehaviour, ILevelController<CollectibleItemLevelSaveModel, CollectibleItemLevelInfoModel>
    {
        [SerializeField] Pooler[] poolers = null;

        List<CollectibleItem> collectibleItems = null;

        public bool HasAnyCollectible => collectibleItems?.Count > 0;

        private CollectibleItem lastCollectibleItem = null;

        public void OnLevelLoad(CollectibleItemLevelSaveModel saveModel)
        {
            collectibleItems = new List<CollectibleItem>();

            SpawnItems(saveModel.GetLevelInfos());

            SetLastCollectibleItem();
        }

        public void UnloadLevel()
        {
            if (!Application.isPlaying && collectibleItems?.Count > 0)
            {
                foreach (CollectibleItem item in collectibleItems)
                {
                    DestroyImmediate(item.gameObject);
                }
            }

            collectibleItems = null;
        }

        public void SpawnItems(CollectibleItemLevelInfoModel[] levelInfos)
        {
            if (levelInfos == null || levelInfos.Length < 1)
                return;

            foreach(CollectibleItemLevelInfoModel info in levelInfos)
            {
                AddItem(info);
            }
        }

        public void AddItem(CollectibleItemLevelInfoModel levelInfo)
        {
            CollectibleItem item = SpawnItem(levelInfo.type, levelInfo.Position);

            if (item == null)
                return;

            collectibleItems.Add(item);
            SetLastCollectibleItem();
        }

        public void RemoveLastItem()
        {
            if (Application.isPlaying || collectibleItems?.Count < 1)
                return;

            CollectibleItem lastCollectibleItem = collectibleItems[collectibleItems.Count - 1];

            collectibleItems.Remove(lastCollectibleItem);
            DestroyImmediate(lastCollectibleItem.gameObject);

            SetLastCollectibleItem();
        }

        public Vector3 GetNextCollectibleItemPosition()
        {
            Vector3 position = Vector3.zero;

            if (lastCollectibleItem != null)
                position = GetLastCollectibleItemPosition();

            position.z += LevelDesigner.COLLECTIBLE_ITEM_SIZE * 2f + LevelDesigner.COLLECTIBLE_ITEM_GAP;

            return position;
        }

        public Vector3 GetLastCollectibleItemPosition() 
        {
            return lastCollectibleItem.transform.position;
        }

        private CollectibleItem SpawnItem(CollectibleItemTypes itemType, Vector3 position)
        {
            Pooler pooler = GetPooler(itemType);

            if (pooler == null)
                return null;

            CollectibleItem item = pooler.GetGo<CollectibleItem>(pooler.transform);

            item.gameObject.SetActive(true);
            item.transform.position = position;

            return item;
        }

        private Pooler GetPooler(CollectibleItemTypes itemType) 
        {
            int index = (int)itemType;

            if (index < 0 || index > (poolers.Length - 1))
                return null;

            return poolers[index];
        }

        private void SetLastCollectibleItem() 
        {
            if (collectibleItems == null || collectibleItems.Count < 1) 
            {
                lastCollectibleItem = null;
                return;
            }

            lastCollectibleItem = collectibleItems[collectibleItems.Count - 1];
        }
    }
}