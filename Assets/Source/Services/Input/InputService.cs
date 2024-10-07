using System;
using Source.Services.Mono;
using Source.Services.ServicesResolver;

namespace Source.Services.Input
{
    public class InputService : BaseService, IInputService
    {
        public event Action OnPointerDown;
        public event Action OnPointerDrag;
        public event Action OnPointerUp;
        
        public InputService(ServiceResolver serviceResolver) : base(serviceResolver) { }

        protected override void Initialize()
        {
            GetService<MonoService>().OnUpdate += OnUpdate;
        }

        public override void Dispose()
        {
            GetService<MonoService>().OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float dt)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnPointerDown?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                OnPointerDrag?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnPointerUp?.Invoke();
            }
        }
    }
}