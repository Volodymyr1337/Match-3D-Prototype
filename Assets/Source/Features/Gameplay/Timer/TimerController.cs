using System;
using Source.Application;

namespace Source.Features.Gameplay.Timer
{
    public class TimerController : BaseViewController<TimerView>
    {
        private Utils.Timer _timer;

        public event Action OnOutOfTime;
        
        public TimerController() : base(nameof(TimerView))
        {
            GameplayController.OnGameOver += OnGameOver;
        }

        public override void Dispose()
        {
            GameplayController.OnGameOver -= OnGameOver;
            base.Dispose();
        }

        public void StartTimer()
        {
            int playTime = 100;
            
            _timer = new Utils.Timer(playTime);
            _timer.OnComplete += OnTimerComplete;
            View.Init(_timer);
            View.SetTime(playTime);
            
            _timer.StartTimer().Forget();
        }
        
        private void OnTimerComplete()
        {
            OnOutOfTime?.Invoke();
        }

        private void OnGameOver(bool result)
        {
            _timer?.StopTimer();
        }
    }
}