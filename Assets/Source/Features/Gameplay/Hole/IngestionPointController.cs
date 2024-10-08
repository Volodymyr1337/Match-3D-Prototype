using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Cards;
using Source.Features.Gameplay.Items;
using Source.Services.Input;
using UnityEngine;

namespace Source.Features.Gameplay.Hole
{
    public class IngestionPointController : BaseViewController<IngestionPointView>
    {
        private ItemView _selectedItem;
        private CardsModel _cardsModel;
        private Action<List<ItemView>> _onCollect;
        
        public IngestionPointController(Action<List<ItemView>> onCollect) : base("IngestionPoint")
        {
            _onCollect = onCollect;
        }

        public override async UniTask Initialize()
        {
            await base.Initialize();
            
            View.OnItemEnter += OnItemEnter;
            View.OnItemExit += OnItemExit;
            View.OnCollect += OnCollect;
            
            CardsModel.OnModelUpdated += OnCardsModelUpdated;
            ServiceResolver.Get<IInputService>().OnPointerUp += OnPointerUp;
        }

        public override void Dispose()
        {
            View.OnItemEnter -= OnItemEnter;
            View.OnItemExit -= OnItemExit;
            View.OnCollect -= OnCollect;
            
            CardsModel.OnModelUpdated -= OnCardsModelUpdated;
            ServiceResolver.Get<IInputService>().OnPointerUp -= OnPointerUp;
            base.Dispose();
        }

        private void OnCollect(List<ItemView> collectedItems)
        {
            _onCollect?.Invoke(collectedItems);
        }

        private void OnPointerUp()
        {
            if (_selectedItem != null)
            {
                View.AddItem(_selectedItem);
            }

            Debug.Log(">>> OnPointerUp " + _selectedItem.name);
            _selectedItem = null;
        }

        private void OnItemEnter(Collider other)
        {
            var itemView = other.gameObject.GetComponent<ItemView>();
            if (itemView != null && itemView.ItemType == ItemType.Apple)
            {
                _selectedItem = itemView;
                Debug.Log(">>> OnItemEnter " + _selectedItem.name);
            }
        }

        private void OnItemExit()
        {
            if (_selectedItem != null)
            {
                View.Remove(_selectedItem);
            }
            _selectedItem = null;
        }

        private void OnCardsModelUpdated(CardsModel cardsModel)
        {
            _cardsModel = cardsModel;
        }
    }
}