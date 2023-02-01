using AppUI.PlayerMove.Scripts;
using AppUI.Popups.Scripts;
using AppUI.TimeTracker.Scripts;
using Core.Game.Scripts;
using Core.Game.Turn.Scripts;
using Core.Player.Scripts;
using UnityEngine;

namespace AppUI.Scripts {
    public class UIManager : MonoBehaviour {
        [Header("Managers")] 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TimeManager _timeManager;

        [Header("Components")] 
        [SerializeField] private TimePresenter _timePresenter;
        [SerializeField] private PlayerMoveHandler _playerMove;

        [Header("Popups")] 
        [SerializeField] private GameEndPopup _gameEndPopupPopup;

        private void OnEnable() {
            _timeManager.CurrentTime += TimeUpdated;

            _gameManager.OnActivePlayerChanged += ActivePlayerChanged;
            _gameManager.OnPlayersMovesSet += PlayersSet;
            _gameManager.OnGameOver += GameOverPopup;
            _gameManager.OnGameRestart += HideGameOverPopup;
            _gameManager.OnGameStart += HideGameOverPopup;
        }

        private void OnDisable() {
            _timeManager.CurrentTime -= TimeUpdated;

            _gameManager.OnActivePlayerChanged -= ActivePlayerChanged;
            _gameManager.OnPlayersMovesSet -= PlayersSet;
            _gameManager.OnGameOver -= GameOverPopup;
            _gameManager.OnGameRestart -= HideGameOverPopup;
            _gameManager.OnGameStart -= HideGameOverPopup;
        }

        private void PlayersSet(IPlayer player, IPlayer otherPlayer) {
            _playerMove.InitializePlayerMove(player, otherPlayer);
        }
        
        private void ActivePlayerChanged(IPlayer player) {
            _playerMove.SetActivePlayerTurn(player);
        }

        private void TimeUpdated(float time) {
            _timePresenter.SetCurrentTime(time);
        }
        
        private void GameOverPopup(IPlayer player) {
            _gameEndPopupPopup.gameObject.SetActive(true);
            _gameEndPopupPopup.SetPlayer(player);
        }

        private void HideGameOverPopup() {
            _gameEndPopupPopup.gameObject.SetActive(false);
        }
    }
}