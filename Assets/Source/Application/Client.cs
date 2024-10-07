using Cysharp.Threading.Tasks;
using Source.Features.Gameplay;
using Source.Features.User;
using Source.Services.AssetBundle;
using Source.Services.Mono;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Application
{
    public class Client : MonoBehaviour
    {
        private readonly ServiceResolver _serviceResolver = new();

        private void Awake()
        {
            InitServices();
            InitControllers().Forget();
        }

        private void OnDestroy()
        {
            _serviceResolver.DisposeAllServices();
        }

        private void InitServices()
        {
            _serviceResolver.Add(new MonoService(_serviceResolver));
            _serviceResolver.Add(new ControllerFactory(_serviceResolver));
            _serviceResolver.Bind<IAssetBundleService, AssetBundleService>(new AssetBundleService(_serviceResolver));
            _serviceResolver.Initialize();
        }

        private async UniTaskVoid InitControllers()
        {
            await new UserController().Initialize();
            await new GameplayController().Initialize();
        }
    }
}
