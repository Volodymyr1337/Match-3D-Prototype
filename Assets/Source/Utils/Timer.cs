using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Utils
{
    public class Timer
    {
        public event Action<int> OnTick;
        public event Action OnComplete;
        public bool IsRunning { get; private set; }

        private float _duration;
        private CancellationTokenSource _cancellationTokenSource;

        public Timer(float duration)
        {
            _duration = duration;
            IsRunning = false;
        }

        
        public async UniTaskVoid StartTimer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            IsRunning = true;
            int remainingTime = (int)_duration;

            try
            {
                while (remainingTime > 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                    remainingTime--;
                    OnTick?.Invoke(remainingTime);
                }

                IsRunning = false;
                OnComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Timer was forcefully stopped.");
            }
            finally
            {
                IsRunning = false;
                
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public void StopTimer()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Reset(int timeDuration)
        {
            _duration = timeDuration;
            IsRunning = false;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }
    }

}