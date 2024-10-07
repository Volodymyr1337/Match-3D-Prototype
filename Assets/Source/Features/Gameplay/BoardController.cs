using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Items;
using Source.Services.AssetBundle;
using UnityEngine;

namespace Source.Features.Gameplay
{
    public class BoardController : BaseController
    {
        private ItemPool _itemPool;
        
        public override async UniTask Initialize()
        {
            var itemAssets = await LoadItemAssets();
            _itemPool = new ItemPool(new ItemFactory(itemAssets));
        }
        
        public void GenerateBoard()
        {
            Dictionary<ItemType, int> itemsToSpawn = new Dictionary<ItemType, int>()
            {
                { ItemType.Acorn, 12 },
                { ItemType.Apple, 12 },
                { ItemType.Banana, 12 },
                { ItemType.Cake, 12 }
            };
            var items = itemsToSpawn.Select(item => (item.Key, item.Value)).ToList();
            foreach ((ItemType type, int amount) item in items)
            {
                for (int i = 0; i < item.amount; i++)
                {
                    _itemPool.GetFromPool(item.type, Vector3.up * 2, Random.rotation);
                }
            }
        }

        private async UniTask<ItemAssetStorage> LoadItemAssets()
        {
            ItemAssetStorage itemAssetStorage =
                await GetService<IAssetBundleService>().LoadAsset<ItemAssetStorage>(nameof(ItemAssetStorage));
            return itemAssetStorage;
        }
    }
}