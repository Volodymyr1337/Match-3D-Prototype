using System;
using DG.Tweening;
using Source.Features.Gameplay.Items;
using UnityEngine;

namespace Source.Features.Gameplay.Cards
{
    public class CardsView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        
        private readonly Vector3 _initialPos = new(0f, 2f, 0f);
        private float _initialScale = 0.75f;
            
        private float _scaleTo = 2.5f;
        private float _animTime = 1f;
        private float _moveToY = -2.6f;
        private Tween _tween;

        public ItemType ItemType => _itemType;

        public void Show()
        {
            gameObject.SetActive(true);
            
            transform.position = _initialPos;
            transform.localScale = Vector3.one * _initialScale;
            
            RunAppearAnimation();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void RunAppearAnimation()
        {
            _tween = DOTween.Sequence()
                .Append(transform.DOScale(_scaleTo, _animTime).SetEase(Ease.InCubic).From(_initialScale))
                .AppendInterval(_animTime)
                .AppendCallback(() =>
                {
                    transform.DOScale(_initialScale, _animTime).SetEase(Ease.OutSine);
                    transform.DOMoveZ(_moveToY, _animTime).SetEase(Ease.OutSine);
                }).Play();
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}