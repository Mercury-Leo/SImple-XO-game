using Core;
using UnityEngine;
using UnityEngine.UI;

namespace AppUI.MoveTracker.Scripts {
    [RequireComponent(typeof(Image))]
    public class MovePresenter : MonoBehaviour {
        private Image _moveImage;
        private bool _modeState;
        private GameConventions.Moves _chosenMove;

        private void Awake() {
            TryGetComponent(out _moveImage);
        }

        public void SetImage(Sprite sprite) {
            _moveImage.overrideSprite = sprite;
        }

        public void SetMove(GameConventions.Moves move) {
            _chosenMove = move;
        }
        
        public void ToggleMode(GameConventions.Moves move) {
            _modeState = move == _chosenMove;
            ChangeMode();
        }

        private void ChangeMode() {
            _moveImage.color = _modeState ? Color.white : Color.gray;
        }
    }
}