using Cysharp.Threading.Tasks;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Source.Services.AssetBundle
{
    public class AssetBundleService : BaseService, IAssetBundleService
    {
        public AssetBundleService(ServiceResolver serviceResolver) : base(serviceResolver)
        {
        }

        protected override void Initialize() { }

        public override void Dispose() { }
        
        public async UniTask<T> LoadAsset<T>(string name) where T : Object
        {
            var resourceRequest = await Resources.LoadAsync<T>(name);
            
            var loadedAsset = resourceRequest as T;

            return loadedAsset;
        }
    }
}