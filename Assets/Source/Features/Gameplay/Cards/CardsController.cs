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

        public override async UniTask Initialize()
        {
            _cardsModel = new CardsModel();
            BoardModel.OnModelUpdated += OnBoardModelUpdated;
            await LoadCardsConfiguration();
        }

        public override void Dispose()
        {
            BoardModel.OnModelUpdated -= OnBoardModelUpdated;
            base.Dispose();
        }

        public void ShowCards()
        {
            var keys = _boardModel.RemainingItemsOnField.Keys.ToArray();
            ItemType randomKey = keys[Random.Range(0, keys.Length)];
            
            if (!_cardsConfiguration.TryGet(randomKey, out GameObject cards))
            {
                Debug.LogError($"Missing cards for type {randomKey}!");
                return;
            }
            
            Object.Instantiate(cards);
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
    }
}