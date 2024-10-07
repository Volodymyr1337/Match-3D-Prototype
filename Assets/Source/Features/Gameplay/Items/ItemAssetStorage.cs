using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    [CreateAssetMenu]
    public class ItemAssetStorage : ScriptableObject
    {
        [SerializeField] private List<ItemAsset> _items = new ();

        public bool TryGet(ItemType type, out GameObject prefab)
        {
            prefab = _items.Find(item => item.type == type)?.prefab;
            return prefab != null;
        }
    }

    [Serializable]
    public class ItemAsset
    {
        public ItemType type;
        public GameObject prefab;
    }
}