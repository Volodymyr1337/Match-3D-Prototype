using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Features.Gameplay
{
    public class PlayBtnView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public static event Action OnPlayBtnClicked;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnPlayBtnClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}