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
        private bool _isPointerUp;
        
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
            ServiceResolver.Get<IInputService>().OnPointerDown += OnPointerDown;
        }

        public override void Dispose()
        {
            View.OnItemEnter -= OnItemEnter;
            View.OnItemExit -= OnItemExit;
            View.OnCollect -= OnCollect;
            
            CardsModel.OnModelUpdated -= OnCardsModelUpdated;
            ServiceResolver.Get<IInputService>().OnPointerUp -= OnPointerUp;
            ServiceResolver.Get<IInputService>().OnPointerDown -= OnPointerDown;
            base.Dispose();
        }

        private void OnCollect(List<ItemView> collectedItems)
        {
            _onCollect?.Invoke(collectedItems);
        }

        private void OnPointerUp()
        {
            _isPointerUp = true;
            if (_selectedItem != null)
            {
                View.AddItem(_selectedItem);
            }

            _selectedItem = null;
        }

        private void OnPointerDown()
        {
            _isPointerUp = false;
        }

        private void OnItemEnter(Collider other)
        {
            if (_isPointerUp) return;
            
            var itemView = other.gameObject.GetComponent<ItemView>();
            if (itemView != null && itemView.ItemType == _cardsModel.TargetItemType)
            {
                _selectedItem = itemView;
            }
        }

        private void OnItemExit(Collider other)
        {
            if (_isPointerUp) return;
            
            var itemView = other.gameObject.GetComponent<ItemView>();
            if (itemView != null)
            {
                View.Remove(itemView);
            }
            _selectedItem = null;
        }

        private void OnCardsModelUpdated(CardsModel cardsModel)
        {
            _cardsModel = cardsModel;
        }
    }
}