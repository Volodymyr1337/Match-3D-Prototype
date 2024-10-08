using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Features.Gameplay.Cards;
using Source.Features.Gameplay.Hole;
using Source.Features.Gameplay.Items;
using Source.Features.Gameplay.Timer;
using Source.Features.User;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {
        private BoardController _boardController;
        private CardsController _cardsController;
        private TimerController _timerController;
        
        private int _level;
        
        public static event Action<bool> OnGameOver;
        
        public override async UniTask Initialize()
        {
            _boardController = CreateController<BoardController>();
            _cardsController = CreateController<CardsController>();
            _timerController = CreateController<TimerController>();
            
            var ingestionPointController = CreateController(new IngestionPointController(OnCollect));
            
            await UniTask.WhenAll(_cardsController.Initialize(), _timerController.Initialize(),
                _boardController.Initialize(), ingestionPointController.Initialize());

            _timerController.OnOutOfTime += OnOutOfTime;
            _boardController.OnAllItemsCollected += OnAllItemsCollected;
            UserModel.OnModelUpdated += OnUserModelUpdated;
            PlayBtnView.OnPlayBtnClicked += StartGame;
            
            CreateController(new ItemMovementController(_boardController.BoardModel.BoardConfiguration)).Initialize();
        }

        public override void Dispose()
        {
            UserModel.OnModelUpdated -= OnUserModelUpdated;
            PlayBtnView.OnPlayBtnClicked -= StartGame;
            
            _timerController.OnOutOfTime -= OnOutOfTime;
            _boardController.OnAllItemsCollected -= OnAllItemsCollected;
            base.Dispose();
        }

        private void OnCollect(List<ItemView> collectedItems)
        {
            _boardController.Collect(collectedItems);
            _cardsController.ShowCards();
        }

        private void OnAllItemsCollected()
        {
            OnGameOver?.Invoke(true);
        }

        private void OnOutOfTime()
        {
            OnGameOver?.Invoke(false);
        }

        public void StartGame()
        {
            _boardController.GenerateBoard();
            _cardsController.ShowCards();
            _timerController.StartTimer();
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        private void OnUserModelUpdated(UserModel userModel)
        {
            SetLevel(userModel.Level);
        }
    }
}