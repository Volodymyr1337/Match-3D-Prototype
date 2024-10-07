using System;
using UnityEngine;

namespace Source.Services.Input
{
    public interface IInputService : IDisposable
    {
        public event Action OnPointerDown;
        public event Action OnPointerDrag;
        public event Action OnPointerUp;
    }
}