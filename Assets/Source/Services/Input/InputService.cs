using System;
using Source.Services.Mono;
using Source.Services.ServicesResolver;
using UnityEngine;

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
            // Handle mouse input for desktop
            if (UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer || 
                UnityEngine.Application.platform == RuntimePlatform.OSXPlayer || 
                UnityEngine.Application.platform == RuntimePlatform.LinuxPlayer || 
                UnityEngine.Application.isEditor) // For testing in Unity editor
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
            // Handle touch input for mobile
            else if (UnityEngine.Application.platform == RuntimePlatform.Android || 
                     UnityEngine.Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (UnityEngine.Input.touchCount > 0)
                {
                    Touch touch = UnityEngine.Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        OnPointerDown?.Invoke();
                    }

                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        OnPointerDrag?.Invoke();
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        OnPointerUp?.Invoke();
                    }
                }
            }
        }

    }
}