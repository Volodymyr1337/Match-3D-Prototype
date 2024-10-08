using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Features.Gameplay.Cards;
using Source.Features.Gameplay.Hole;
using Source.Features.Gameplay.Items;
using Source.Features.User;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {
        private BoardController _boardController;
        private CardsController _cardsController;
        private int _level;
        
        public static event Action<bool> OnGameOver;
        
        public override async UniTask Initialize()
        {
            _boardController = CreateController<BoardController>();
            _cardsController = CreateController<CardsController>();
            var ingestionPointController = CreateController(new IngestionPointController(OnCollect));
            
            await UniTask.WhenAll(_cardsController.Initialize(), 
                _boardController.Initialize(), ingestionPointController.Initialize());
            
            _boardController.OnAllItemsCollected += OnAllItemsCollected;
            UserModel.OnModelUpdated += OnUserModelUpdated;
            
            CreateController(new ItemMovementController(_boardController.BoardModel.BoardConfiguration)).Initialize();
        }

        public override void Dispose()
        {
            UserModel.OnModelUpdated -= OnUserModelUpdated;
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

        public void StartGame()
        {
            _boardController.GenerateBoard();
            _cardsController.ShowCards();
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        private void OnUserModelUpdated(UserModel userModel)
        {
            SetLevel(userModel.Level);
        }

        // var timer = new Timer(10);
        // timer.OnComplete += OnTimerComplete;
        // timer.StartTimer().Forget();
        // private void OnTimerComplete()
        // {
        //     Debug.Log("game over!");
        // }
    }
}