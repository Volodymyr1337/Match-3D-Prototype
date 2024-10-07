using System;
using UnityEngine;

namespace Source.Services.Mono
{
    public class MonoComponent : MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action<bool> OnApplicationPaused;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void OnApplicationPause(bool isPaused)
        {
            OnApplicationPaused?.Invoke(isPaused);
        }
    }
}
