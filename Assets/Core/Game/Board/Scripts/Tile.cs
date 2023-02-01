using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Game.Board.Scripts {
    /// <summary>
    /// Represents a simple tile on the game board
    /// Holds its index on the board
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Tile : MonoBehaviour, IPointerClickHandler {
        private bool _isSet;
        private Image _image;
        
        public int Index { get; private set; }
        
        public Action<int> OnClick { get; set; }

        private void Awake() {
            TryGetComponent(out _image);
            Index = transform.GetSiblingIndex();
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (_isSet)
                return;
            OnClick?.Invoke(Index);
        }

        public void SetSprite(Sprite sprite) {
            if (_isSet)
                return;
            _image.overrideSprite = sprite;
            _isSet = true;
        }

        public void ClearTile() {
            _image.overrideSprite = null;
            _isSet = false;
        }
    }
}