using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Items;
using Source.Services.AssetBundle;

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
            
        }

        private async UniTask<ItemAssetStorage> LoadItemAssets()
        {
            var assetBundleService = ServiceResolver.Get<IAssetBundleService>();
            ItemAssetStorage itemAssetStorage =
                await assetBundleService.LoadAsset<ItemAssetStorage>(nameof(ItemAssetStorage));
            return itemAssetStorage;
        }
    }
}