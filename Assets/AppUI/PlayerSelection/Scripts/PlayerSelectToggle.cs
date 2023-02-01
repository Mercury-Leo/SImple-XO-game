using System;
using UnityEngine;
using UnityEngine.UI;

namespace AppUI.PlayerSelection.Scripts {
    [RequireComponent(typeof(Toggle))]
    public class PlayerSelectToggle : MonoBehaviour {
        [SerializeField] private bool _usesInput;

        private Toggle _toggle;
        
        public Toggle Toggle => _toggle ??= GetComponent<Toggle>();

        public Action<bool> OnPlayerSelected { get; set; }

        private void OnEnable() {
            Toggle.onValueChanged?.AddListener(PlayerSelected);
        }

        private void OnDisable() {
            Toggle.onValueChanged?.RemoveListener(PlayerSelected);
        }

        private void PlayerSelected(bool state) {
            OnPlayerSelected?.Invoke(_usesInput);
        }
    }
}
