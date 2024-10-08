using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Features.Gameplay.Items;
using Source.Services.AssetBundle;
using UnityEngine;

namespace Source.Features.Gameplay.Cards
{
    public class CardsController : BaseController
    {
        private CardsModel _cardsModel;
        private BoardModel _boardModel;
        private CardsConfiguration _cardsConfiguration;
        private Dictionary<ItemType, CardsView> _cardViews = new Dictionary<ItemType, CardsView>();

        public override async UniTask Initialize()
        {
            _cardsModel = new CardsModel();
            BoardModel.OnModelUpdated += OnBoardModelUpdated;
            GameplayController.OnStartGame += ShowCards;
            GameplayController.OnCollectItems += ShowCards;
            await LoadCardsConfiguration();
        }

        public override void Dispose()
        {
            BoardModel.OnModelUpdated -= OnBoardModelUpdated;
            GameplayController.OnStartGame -= ShowCards;
            GameplayController.OnCollectItems -= ShowCards;
            base.Dispose();
        }

        private async void ShowCards()
        {
            await UniTask.WaitUntil(() => _boardModel.RemainingItemsOnField.Count > 0);
            
            var keys = _boardModel.RemainingItemsOnField.Keys.ToArray();
            if (keys.Length == 0) return;
            
            ItemType randomKey = keys[Random.Range(0, keys.Length)];
            CreateCards(randomKey);
            
            if (_cardViews.ContainsKey(_cardsModel.TargetItemType))
            {
                _cardViews[_cardsModel.TargetItemType].Hide();
            }
            _cardViews[randomKey].Show();
            _cardsModel.TargetItem(randomKey);
        }
        
        private void OnBoardModelUpdated(BoardModel model)
        {
            _boardModel = model;
        }
        
        private async UniTask LoadCardsConfiguration()
        {
            _cardsConfiguration =
                await GetService<IAssetBundleService>().LoadAsset<CardsConfiguration>(nameof(CardsConfiguration));
        }

        private void CreateCards(ItemType itemType)
        {
            if (!_cardViews.ContainsKey(itemType))
            {
                if (!_cardsConfiguration.TryGet(itemType, out CardsView cardsPrefab))
                {
                    Debug.LogError($"Missing cards for type {itemType}!");
                    return;
                }
                CardsView cardsView = Object.Instantiate(cardsPrefab);
            
                _cardViews.Add(itemType, cardsView);
            }
        }
    }
}