using Cysharp.Threading.Tasks;
using Source.Services.AssetBundle;
using UnityEngine;

namespace Source.Application
{
    public abstract class BaseViewController<TView>: BaseController where TView: MonoBehaviour
    {
        protected readonly string _assetName;
        
        protected TView View { get; private set; }
        
        protected BaseViewController(string assetName)
        {
            _assetName = assetName;
        }

        public override async UniTask Initialize()
        {
            await LoadView();
        }

        private async UniTask LoadView()
        {
            var assetBundleService = ServiceResolver.Get<IAssetBundleService>();
            var resourceRequest = await assetBundleService.LoadAsset<TView>(_assetName);

            // Once the resource is loaded, instantiate it
            TView loadedAsset = resourceRequest as TView;
            if (loadedAsset != null)
            {
                View = Object.Instantiate(loadedAsset);
            }
            else
            {
                Debug.LogError($"Failed to load resource: {_assetName}");
            }
        }

        public override void Dispose()
        {
            if (View != null)
            {
                Object.Destroy(View.gameObject);
            }
        }
    }
}
