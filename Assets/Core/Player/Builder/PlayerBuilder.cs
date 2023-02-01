using Core.Player.Factory;
using Core.Player.Scripts;
using Core.PlayerSelection.Scripts;
using UnityEngine;

namespace Core.Player.Builder {
    public class PlayerBuilder : MonoBehaviour {
        [SerializeField] private PlayerSelectionSO _playerSelection;
        
        private readonly PlayerInputFactory _playerInputFactory = new PlayerInputFactory();
        private readonly PlayerComputerFactory _playerComputerFactory = new PlayerComputerFactory();

        private IPlayer _player;
        private IPlayer _otherPlayer;
        
        private void Awake() {
            CreatePlayers();
        }

        private void CreatePlayers() {
            var inputs = _playerSelection.GetPlayers();
            if (inputs.playerInput is null || inputs.otherPlayerInput is null) {
                _player = _playerInputFactory.CreatePlayer();
                _otherPlayer = _playerInputFactory.CreatePlayer();
                return;
            }

            _player = (bool)inputs.playerInput ? _playerInputFactory.CreatePlayer() : _playerComputerFactory.CreatePlayer();
            _otherPlayer = (bool)inputs.otherPlayerInput ? _playerInputFactory.CreatePlayer() : _playerComputerFactory.CreatePlayer();
        }

        public (IPlayer player, IPlayer otherPlayer) GetPlayers() {
            return (_player, _otherPlayer);
        }
    }
}
