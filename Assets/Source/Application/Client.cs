using Cysharp.Threading.Tasks;
using Source.Features.Gameplay;
using Source.Features.Gameplay.EndGame;
using Source.Features.TopBar;
using Source.Features.User;
using Source.Services.AssetBundle;
using Source.Services.Input;
using Source.Services.Mono;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Source.Application
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
            _serviceResolver.Bind<IInputService, InputService>(new InputService(_serviceResolver));
            _serviceResolver.Bind<IAssetBundleService, AssetBundleService>(new AssetBundleService(_serviceResolver));
            
            _serviceResolver.Initialize();
        }

        private async UniTaskVoid InitControllers()
        {
            var controllerFactory = _serviceResolver.Get<ControllerFactory>();
            var userController = controllerFactory.CreateController<UserController>();
            var gameplayController = controllerFactory.CreateController<GameplayController>();
            var topBarController = controllerFactory.CreateController<TopBarController>();
            
            var initUserController = userController.Initialize();
            var initGameplayController = gameplayController.Initialize();
            var initTopBar = topBarController.Initialize();
            
            await UniTask.WhenAll(initUserController, initGameplayController, initTopBar);

            controllerFactory.CreateController(new EndGameController(userController.UserModel)).Initialize();
            gameplayController.StartGame();
        }
    }
}
