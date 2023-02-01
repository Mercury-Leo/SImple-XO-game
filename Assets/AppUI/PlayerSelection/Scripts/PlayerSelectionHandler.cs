using Core;
using Core.Extensions.Scene;
using Core.PlayerSelection.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace AppUI.PlayerSelection.Scripts {
    public class PlayerSelectionHandler : MonoBehaviour {
        [SerializeField] private PlayerSelectionSO _playerSelection;

        [Header("Player Selectors")]
        [SerializeField] private PlayerSelector _playerSelector;
        [SerializeField] private PlayerSelector _otherPlayerSelector;

        [Header("Components")]
        [SerializeField] private Button _playButton;

        private bool? _playerInput;
        private bool? _otherPlayerInput;

        private void Awake() {
            _playButton.interactable = false;
        }

        private void OnEnable() {
            _playerSelector.OnPlayerSet += SetPlayer;
            _otherPlayerSelector.OnPlayerSet += SetOtherPlayer;
            _playButton.onClick?.AddListener(PlayGame);
        }

        private void OnDisable() {
            _playerSelector.OnPlayerSet -= SetPlayer;
            _otherPlayerSelector.OnPlayerSet -= SetOtherPlayer;
            _playButton.onClick?.RemoveListener(PlayGame);
        }

        private void PlayGame() {
            SceneExtensions.LoadSceneByMode(GameConventions.Scenes.Game.GetName());
        }

        private void SetPlayer(bool isInput) {
            _playerInput = isInput;
            SetPlayers();
        }

        private void SetOtherPlayer(bool isInput) {
            _otherPlayerInput = isInput;
            SetPlayers();
        }


        private void SetPlayers() {
            if (_playerInput is null || _otherPlayerInput is null)
                return;

            _playButton.interactable = true;
            _playerSelection.SetPlayers(_playerInput, _otherPlayerInput);
        }
    }
}