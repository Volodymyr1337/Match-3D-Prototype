using Source.Services.ServicesResolver;

namespace Source.Services.AssetBundle
{
    public class AssetBundleService : BaseService, IAssetBundleService
    {
        public AssetBundleService(ServiceResolver serviceResolver) : base(serviceResolver)
        {
        }

        protected override void Initialize() { }

        public override void Dispose() { }
    }
}