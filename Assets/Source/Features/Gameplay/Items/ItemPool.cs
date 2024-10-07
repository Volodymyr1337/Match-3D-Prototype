using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    public class ItemPool
    {
        private const int POOL_SIZE = 10;
        
        private readonly Dictionary<ItemType, Queue<GameObject>> _poolDictionary;

        private readonly ItemFactory _itemFactory;

        public ItemPool(ItemFactory itemFactory)
        {
            _poolDictionary = new Dictionary<ItemType, Queue<GameObject>>();
            _itemFactory = itemFactory;
            Initialize();
        }

        private void Initialize()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < POOL_SIZE; i++)
                {
                    if (_itemFactory.TryCreateItem(itemType, out GameObject item))
                    {
                        item.SetActive(false);
                        objectPool.Enqueue(item);
                    }
                }
                
                _poolDictionary.Add(itemType, objectPool);
            }
        }
        
        public GameObject GetFromPool(ItemType itemType, Vector3 position, Quaternion rotation)
        {
            // Check if we have an available object in the pool
            if (_poolDictionary[itemType].Count == 0)
            {
                if (_itemFactory.TryCreateItem(itemType, out GameObject item))
                {
                    item.SetActive(false);
                    _poolDictionary[itemType].Enqueue(item);
                }
            }
                
            GameObject spawnedItem = _poolDictionary[itemType].Dequeue();

            spawnedItem.SetActive(true);
            spawnedItem.transform.position = position;
            spawnedItem.transform.rotation = rotation;

            return spawnedItem;
        }

        public void ReturnToPool(ItemType objectType, GameObject item)
        {
            item.SetActive(false);
            _poolDictionary[objectType].Enqueue(item);
        }
    }
}