using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Features.Gameplay.EndGame
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _closeBtn;

        private void Awake()
        {
            _closeBtn.onClick.AddListener(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            _closeBtn.onClick.RemoveAllListeners();
        }
    }
}