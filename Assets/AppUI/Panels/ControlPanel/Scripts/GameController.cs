using Core;
using Core.Extensions.Scene;
using Core.Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace AppUI.Panels.ControlPanel.Scripts {
    /// <summary>
    /// Controls the game via a set of buttons
    /// </summary>
    public class GameController : MonoBehaviour {
        [SerializeField] private GameManager _gameManager;

        [Header("Buttons")] 
        [SerializeField] private Button _play;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _undo;
        [SerializeField] private Button _home;

        private void OnEnable() {
            _play.onClick?.AddListener(StartGame);
            _restart.onClick?.AddListener(RestartGame);
            _undo.onClick?.AddListener(UndoMove);
            _home.onClick?.AddListener(ReturnToMainMenu);
            _gameManager.OnPlayerVsComputer += SetUndoButton;
        }

        private void OnDisable() {
            _play.onClick?.RemoveListener(StartGame);
            _restart.onClick?.RemoveListener(RestartGame);
            _undo.onClick?.RemoveListener(UndoMove);
            _home.onClick?.RemoveListener(ReturnToMainMenu);
            _gameManager.OnPlayerVsComputer -= SetUndoButton;

        }

        private void StartGame() {
            _gameManager.StartGame();
        }

        private void RestartGame() {
            _gameManager.RestartGame();
        }

        private void UndoMove() {
            _gameManager.UndoMove();
        }

        private void ReturnToMainMenu() {
            SceneExtensions.LoadSceneByMode(GameConventions.Scenes.MainMenu.GetName());
        }

        private void SetUndoButton(bool state) {
            _undo.interactable = state;
        }
    }
}