using System;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Source.Services.Mono
{
    public class MonoService : BaseService
    {
        private MonoComponent _monoComponent;

        public event Action<float> OnUpdate;
        public event Action<bool> OnApplicationPaused;
        
        public MonoService(ServiceResolver serviceResolver) : base(serviceResolver) { }
        
        protected override void Initialize()
        {
            _monoComponent = new GameObject("Mono").AddComponent<MonoComponent>();
            _monoComponent.OnUpdate += Update;
            _monoComponent.OnApplicationPaused += OnPause;
        }

        public override void Dispose()
        {
            if (_monoComponent != null)
            {
                _monoComponent.OnUpdate -= Update;
                _monoComponent.OnApplicationPaused -= OnPause;
            }
        }

        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime);
        }

        private void OnPause(bool isPaused)
        {
            OnApplicationPaused?.Invoke(isPaused);
        }
    }
}
