using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Features.Gameplay.Cards;
using Source.Features.Gameplay.Hole;
using Source.Features.Gameplay.Items;
using Source.Features.Gameplay.LevelConfiguration;
using Source.Features.Gameplay.Timer;
using Source.Services.AssetBundle;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {
        private BoardController _boardController;
        private TimerController _timerController;
        private LevelConfig _levelConfig;
        private bool _isActiveGame;

        public static event Action<bool> OnGameOver;
        public static event Action OnCollectItems;
        public static event Action OnStartGame;
        
        public override async UniTask Initialize()
        {
            _boardController = CreateController<BoardController>();
            _timerController = CreateController<TimerController>();
            
            var cardsController = CreateController<CardsController>();
            var ingestionPointController = CreateController(new IngestionPointController(OnCollect));
            
            await UniTask.WhenAll(LoadLevelConfig(), _boardController.Initialize(), _timerController.Initialize(),
                ingestionPointController.Initialize(), cardsController.Initialize());

            _boardController.SetLevelConfig(_levelConfig);
            _timerController.OnOutOfTime += OnOutOfTime;
            _boardController.OnAllItemsCollected += OnAllItemsCollected;
            
            PlayBtnView.OnPlayBtnClicked += StartGame;
            
            CreateController(new ItemMovementController(_boardController.BoardModel.BoardConfiguration)).Initialize();
        }

        public override void Dispose()
        {
            PlayBtnView.OnPlayBtnClicked -= StartGame;
            
            _timerController.OnOutOfTime -= OnOutOfTime;
            _boardController.OnAllItemsCollected -= OnAllItemsCollected;
            base.Dispose();
        }

        public void StartGame()
        {
            _isActiveGame = true;
            _timerController.StartTimer(_levelConfig.duration);
            OnStartGame?.Invoke();
        }

        private void OnCollect(List<ItemView> collectedItems)
        {
            _boardController.Collect(collectedItems);
            OnCollectItems?.Invoke();
        }

        private void OnAllItemsCollected()
        {
            if (!_isActiveGame) return;
            
            _isActiveGame = false;
            OnGameOver?.Invoke(true);
        }

        private void OnOutOfTime()
        {
            if (!_isActiveGame) return;
            
            _isActiveGame = false;
            OnGameOver?.Invoke(false);
        }
        
        private async UniTask LoadLevelConfig()
        {
            _levelConfig = await GetService<IAssetBundleService>().LoadAsset<LevelConfig>("DefaultLevelConfig");
        }
    }
}