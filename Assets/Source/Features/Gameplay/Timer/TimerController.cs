using System;
using Cysharp.Threading.Tasks;
using Source.Application;
using UnityEngine;

namespace Source.Features.Gameplay.Timer
{
    public class TimerController : BaseViewController<TimerView>
    {
        private Utils.Timer _timer;

        public event Action OnOutOfTime;
        
        public TimerController() : base(nameof(TimerView))
        {
        }

        public void StartTimer()
        {
            int playTime = 10;
            _timer = new Utils.Timer(playTime);
            _timer.OnComplete += OnTimerComplete;
            View.Init(_timer);
            View.SetTime(playTime);
            
            _timer.StartTimer().Forget();
        }
        
        private void OnTimerComplete()
        {
            
        }
    }
}