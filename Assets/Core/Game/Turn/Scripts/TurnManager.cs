using System;
using UnityEngine;

namespace Core.Game.Turn.Scripts {
    public class TurnManager : MonoBehaviour {
        [SerializeField] private TimeManager _timeManager;

        public Action OnTurnBegin { get; set; }
        public Action OnTurnEnded { get; set; }
        public Action OnTurnLose { get; set; }

        private void Awake() {
            _timeManager.TimeToCount = GameConventions.TurnTime;
        }

        private void OnEnable() {
            _timeManager.OnTimerFinished += OnTimerFinished;
            _timeManager.OnTimerBegin += OnTimerBegin;
            _timeManager.OnTimerEnd += OnTimerEnd;
        }

        private void OnDisable() {
            _timeManager.OnTimerFinished -= OnTimerFinished;
            _timeManager.OnTimerBegin -= OnTimerBegin;
            _timeManager.OnTimerEnd -= OnTimerEnd;
        }

        public void BeginTurn() {
            _timeManager.BeginCounting();
        }

        public void RestartTurn() {
            _timeManager.BeginCounting();
        }

        public void StopTurns() {
            _timeManager.StopCounting();
        }

        private void OnTimerFinished() {
            OnTurnLose?.Invoke();
        }
        
        private void OnTimerBegin() {
            OnTurnBegin?.Invoke();
        }
        
        private void OnTimerEnd() {
            OnTurnEnded?.Invoke();
        }
    }
}