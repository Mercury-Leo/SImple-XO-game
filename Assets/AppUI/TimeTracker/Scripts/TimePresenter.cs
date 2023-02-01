using System;
using Core;
using TMPro;
using UnityEngine;

namespace AppUI.TimeTracker.Scripts {
    [RequireComponent(typeof(TMP_Text))]
    public class TimePresenter : MonoBehaviour {
        private TMP_Text _timeText;

        private const string TimeTitle = "Time: ";

        private void Awake() {
            TryGetComponent(out _timeText);
        }

        private string CurrentTime {
            set => _timeText.text = TimeTitle + value;
        }

        public void SetCurrentTime(float time) {
            CurrentTime = TimeSpan.FromSeconds(time).ToString(GameConventions.TimeFormat);
        }
    }
}