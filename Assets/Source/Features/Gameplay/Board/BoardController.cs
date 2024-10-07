using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Items;
using Source.Services.AssetBundle;
using UnityEngine;

namespace Source.Features.Gameplay.Board
{
    public class BoardController : BaseController
    {
        private ItemPool _itemPool;
        private BoardConfiguration _boardConfiguration;
        
        public override async UniTask Initialize()
        {
            var loadItemAssetsRequest = LoadItemAssets();
            var loadBoardConfigRequest = LoadBoardConfiguration();
            var (loadedItems, boardConfiguration) = await UniTask.WhenAll(loadItemAssetsRequest, loadBoardConfigRequest);
            
            _itemPool = new ItemPool(new ItemFactory(loadedItems));
            _boardConfiguration = boardConfiguration;
        }
        
        public void GenerateBoard()
        {
            Vector2 spawnAreaSize = _boardConfiguration.Area;
            float xOffset = _boardConfiguration.Offset.x;
            float yOffset = _boardConfiguration.Offset.y;
            
            float halfSpawnAreaWidth = spawnAreaSize.x * 0.5f;
            float halfSpawnAreaHeight = spawnAreaSize.y * 0.5f;
            
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
                for (int i = 0; i < item.amount * 2; i++)
                {
                    float xPos = Random.Range(-halfSpawnAreaWidth, halfSpawnAreaWidth) + xOffset;
                    float yPos = Random.Range(-halfSpawnAreaHeight, halfSpawnAreaHeight) + yOffset;
                    Vector3 spawnPosition = new Vector3(xPos, 2f,  yPos);
                    
                    _itemPool.GetFromPool(item.type, spawnPosition, Quaternion.identity);
                }
            }
        }

        private async UniTask<ItemAssetStorage> LoadItemAssets()
        {
            ItemAssetStorage itemAssetStorage =
                await GetService<IAssetBundleService>().LoadAsset<ItemAssetStorage>(nameof(ItemAssetStorage));
            return itemAssetStorage;
        }
        
        private async UniTask<BoardConfiguration> LoadBoardConfiguration()
        {
            BoardConfiguration itemAssetStorage =
                await GetService<IAssetBundleService>().LoadAsset<BoardConfiguration>(nameof(BoardConfiguration));
            return itemAssetStorage;
        }
    }
}