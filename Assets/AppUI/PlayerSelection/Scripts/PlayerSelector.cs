using System;
using UnityEngine;

namespace AppUI.PlayerSelection.Scripts {
    public class PlayerSelector : MonoBehaviour
    {
        [Header("Players")]
        [SerializeField] private PlayerSelectToggle _player;
        [SerializeField] private PlayerSelectToggle _otherPlayer;
        
        public Action<bool> OnPlayerSet { get; set; }

        private void OnEnable() {
            _player.OnPlayerSelected += OnPlayerSelected;
            _otherPlayer.OnPlayerSelected += OnPlayerSelected;
        }

        private void OnDisable() {
            _player.OnPlayerSelected -= OnPlayerSelected;
            _otherPlayer.OnPlayerSelected -= OnPlayerSelected;
        }

        private void OnPlayerSelected(bool usesInput) {
            OnPlayerSet?.Invoke(usesInput);
        }
    }
}
