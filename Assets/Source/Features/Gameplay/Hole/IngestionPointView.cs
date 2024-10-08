using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Source.Features.Gameplay.Items;
using UnityEngine;

namespace Source.Features.Gameplay.Hole
{
    public class IngestionPointView : MonoBehaviour
    {
        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;

        private List<ItemView> _items = new List<ItemView>();
        
        public event Action<Collider> OnItemEnter;
        public event Action<Collider> OnItemExit;
        public event Action<List<ItemView>> OnCollect;
        
        private bool _collecting = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_collecting) return;
            
            OnItemEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (_collecting) return;
            
            OnItemExit?.Invoke(other);
        }

        public void AddItem(ItemView itemView)
        {
            if (_collecting) return;
            
            if (_items.Count > 0)
            {
                _items.Add(itemView);
                itemView.Rigidbody.isKinematic = true;
                itemView.transform.position = _rightPoint.position;
                RunHidingAnimation();
                return;
            }

            _items.Add(itemView);
            itemView.Rigidbody.isKinematic = true;
            itemView.transform.position = _leftPoint.position;
        }

        public void Remove(ItemView itemView)
        {
            if (_collecting) return;
            
            itemView.Rigidbody.isKinematic = false;
            _items.Remove(itemView);
        }

        private async void RunHidingAnimation()
        {
            _collecting = true;
            
            Vector3 midpoint = (_leftPoint.position + _rightPoint.position) / 2;
            float animationTime = 0.5f;
            List<Task> tweens = new List<Task>();
            foreach (ItemView itemView in _items)
            {
                tweens.Add(itemView.transform.DOMove(midpoint, animationTime)
                    .SetEase(Ease.InCubic).AsyncWaitForCompletion());
                tweens.Add(itemView.transform.DOScale(2f * Vector3.one, animationTime)
                    .SetEase(Ease.InCubic).AsyncWaitForCompletion());
            }
            
            await Task.WhenAll(tweens);
            DOVirtual.DelayedCall(animationTime, () =>
            {
                OnCollect?.Invoke(_items);
                _items.Clear();
                _collecting = false;
            });
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}