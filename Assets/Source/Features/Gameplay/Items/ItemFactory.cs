using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    public class ItemFactory
    {
        private readonly ItemAssetStorage _itemStorage;

        public ItemFactory(ItemAssetStorage storage)
        {
            _itemStorage = storage;
        }

        public bool TryCreateItem(ItemType itemType, out GameObject item)
        {
            item = null;
            if (!_itemStorage.TryGet(itemType, out GameObject prefab))
            {
                item = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }

            return item != null;
        }
    }
}