using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    public class ItemFactory
    {
        private readonly ItemAssetStorage _itemStorage;
        private Transform _parent;

        public ItemFactory(ItemAssetStorage storage)
        {
            _itemStorage = storage;
            _parent = new GameObject("Items").transform;
            _parent.position = Vector3.zero;
        }

        public bool TryCreateItem(ItemType itemType, out GameObject item)
        {
            item = null;
            if (_itemStorage.TryGet(itemType, out GameObject prefab))
            {
                item = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, _parent);
            }

            return item != null;
        }
    }
}