using System;
using UnityEngine;

namespace Core.Game.Turn.Scripts {
    public class TimeManager : MonoBehaviour {
        private bool _isTimeCounting;
        private float _timeCounter;

        private float TimeCounter {
            get => _timeCounter;
            set {
                _timeCounter = value;
                CurrentTime?.Invoke(value);
            }
        }

        public float TimeToCount { get; set; } = 5f;
        
        public Action OnTimerBegin { get; set; }
        public Action OnTimerEnd { get; set; }
        public Action OnTimerFinished { get; set; }
        public Action<float> CurrentTime { get; set; }

        private void Update() {
            CountTime();
        }

        public void BeginCounting() {
            SetTimer();
            _isTimeCounting = true;
            OnTimerBegin?.Invoke();
        }

        public void StopCounting() {
            TimeCounter = 0;
            _isTimeCounting = false;
            OnTimerEnd?.Invoke();
        }
        
        private void SetTimer() {
            TimeCounter = TimeToCount;
        }

        private void CountTime() {
            if (!_isTimeCounting)
                return;

            if (TimeCounter > 0) {
                TimeCounter -= Time.deltaTime;
                return;
            }
            
            TimeCounter = 0;
            OnTimerFinished?.Invoke();
        }
    }
}