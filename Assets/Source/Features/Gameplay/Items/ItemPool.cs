using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    public class ItemPool
    {
        private readonly Dictionary<ItemType, Queue<GameObject>> _poolDictionary;

        private readonly ItemFactory _itemFactory;

        public ItemPool(ItemFactory itemStorage)
        {
            _poolDictionary = new Dictionary<ItemType, Queue<GameObject>>();
            _itemFactory = itemStorage;
        }

        private void Initialize()
        {
            
            int poolSize = 10;
            
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < poolSize; i++)
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
            GameObject itemToSpawn;

            // Check if we have an available object in the pool
            if (_poolDictionary[itemType].Count == 0)
            {
                if (_itemFactory.TryCreateItem(itemType, out GameObject item))
                {
                    item.SetActive(false);
                    _poolDictionary[itemType].Enqueue(item);
                }
            }
                
            itemToSpawn = _poolDictionary[itemType].Dequeue();

            itemToSpawn.SetActive(true);
            itemToSpawn.transform.position = position;
            itemToSpawn.transform.rotation = rotation;

            return itemToSpawn;
        }

        public void ReturnToPool(ItemType objectType, GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            _poolDictionary[objectType].Enqueue(objectToReturn);
        }
    }
}