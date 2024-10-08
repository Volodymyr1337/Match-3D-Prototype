using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Items;
using Source.Services.AssetBundle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Features.Gameplay.Board
{
    public class BoardController : BaseController
    {
        public event Action OnAllItemsCollected;
        
        private ItemPool _itemPool;
        public BoardModel BoardModel { get; private set; }
        private Dictionary<ItemType, List<GameObject>> _spawnedItems = new();

        public override async UniTask Initialize()
        {
            var loadItemAssetsRequest = LoadItemAssets();
            var loadBoardConfigRequest = LoadBoardConfiguration();
            var (loadedItems, boardConfiguration) = await UniTask.WhenAll(loadItemAssetsRequest, loadBoardConfigRequest);
            
            _itemPool = new ItemPool(new ItemFactory(loadedItems));
            BoardModel = new BoardModel(boardConfiguration);
        }
        
        public void GenerateBoard()
        {
            Vector2 spawnAreaSize = BoardModel.BoardConfiguration.Area;
            float xOffset = BoardModel.BoardConfiguration.Offset.x;
            float yOffset = BoardModel.BoardConfiguration.Offset.y;
            
            float halfSpawnAreaWidth = spawnAreaSize.x * 0.5f;
            float halfSpawnAreaHeight = spawnAreaSize.y * 0.5f;
            
            // TODO load level config
            Dictionary<ItemType, int> itemsToSpawn = new Dictionary<ItemType, int>()
            {
                { ItemType.Acorn, 12 },
                { ItemType.Apple, 12 },
                { ItemType.Banana, 12 },
                { ItemType.Cake, 12 }
            };
            
            ResetBoard();
            var items = itemsToSpawn.Select(item => (item.Key, item.Value)).ToList();
            foreach ((ItemType type, int amount) item in items)
            {
                List<GameObject> spawnedItems = new List<GameObject>();
                for (int i = 0; i < item.amount * 2; i++)
                {
                    float xPos = Random.Range(-halfSpawnAreaWidth, halfSpawnAreaWidth) + xOffset;
                    float yPos = Random.Range(-halfSpawnAreaHeight, halfSpawnAreaHeight) + yOffset;
                    Vector3 spawnPosition = new Vector3(xPos, 2f,  yPos);
                    
                    var spawnedItem = _itemPool.GetFromPool(item.type, spawnPosition, Quaternion.identity);
                    spawnedItems.Add(spawnedItem);
                }
                _spawnedItems.Add(item.type, spawnedItems);
                BoardModel.RemainingItemsOnField.Add(item.type, item.amount);
            }
            BoardModel.UpdateModel();
        }

        public void Collect(List<ItemView> collectedItems)
        {
            foreach (ItemView collectedItem in collectedItems)
            {
                BoardModel.RemainingItemsOnField[collectedItem.ItemType]--;
                if (BoardModel.RemainingItemsOnField[collectedItem.ItemType] <= 0)
                {
                    BoardModel.RemainingItemsOnField.Remove(collectedItem.ItemType);
                }
                _itemPool.ReturnToPool(collectedItem.ItemType, collectedItem.gameObject);
            }
            BoardModel.UpdateModel();

            CheckRemainingItemCount();
        }

        private void CheckRemainingItemCount()
        {
            int remainingItemCount = BoardModel.RemainingItemsOnField.Select(item => item.Value).Sum();
            if (remainingItemCount <= 0)
            {
                OnAllItemsCollected?.Invoke();
            }
        }

        private void ResetBoard()
        {
            BoardModel.RemainingItemsOnField.Clear();
            foreach (var spawnedItems in _spawnedItems)
            {
                foreach (GameObject spawnedItem in spawnedItems.Value)
                {
                    _itemPool.ReturnToPool(spawnedItems.Key, spawnedItem);
                }
            }
            _spawnedItems.Clear();
        }

        // private UniTask LoadLevelConfig(int level)
        // {
        //     
        // }

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