using UnityEngine;

namespace Core.PlayerSelection.Scripts {
    /// <summary>
    /// Scriptable Object that holds the selected player types
    /// </summary>
    [CreateAssetMenu(menuName = "Player/PlayerSelectionSO", fileName = "new PlayerSelection")]
    public class PlayerSelectionSO : ScriptableObject {
        private bool? _player;
        private bool? _otherPlayer;

        public void SetPlayers(bool? player, bool? otherPlayer) {
            if (player is null || otherPlayer is null)
                return;
            _player = player;
            _otherPlayer = otherPlayer;
        }

        public (bool? playerInput, bool? otherPlayerInput) GetPlayers() {
            return (_player, _otherPlayer);
        }
    }
}