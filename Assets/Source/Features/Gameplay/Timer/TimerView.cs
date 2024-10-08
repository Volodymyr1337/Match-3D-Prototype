using System;
using TMPro;
using UnityEngine;

namespace Source.Features.Gameplay.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        
        private Utils.Timer _timer;
        
        public void Init(Utils.Timer timer)
        {
            _timer = timer;
            _timer.OnTick += SetTime;
        }

        private void OnDestroy()
        {
            _timer.OnTick -= SetTime;
        }

        public void SetTime(int remainingTime)
        {
            var timeSpan = TimeSpan.FromSeconds(remainingTime);
            _timerText.SetText($"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}");
        }
    }
}