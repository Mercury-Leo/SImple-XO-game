using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.SpriteDataBase.Scripts {
    [CreateAssetMenu(menuName = "DataBase/Sprite/SpriteDataBaseSO", fileName = "new SpriteDataBase")]
    public class SpriteDataBaseSO : ScriptableObject {
        [SerializeField] private AssetReferenceSprite _moveXRef;
        [SerializeField] private AssetReferenceSprite _moveORef;

        private AsyncOperationHandle<Sprite> _spriteOOperation;
        private AsyncOperationHandle<Sprite> _spriteXOperation;
        private Sprite _moveX;
        private Sprite _moveO;

        /// <summary>
        /// Load the sprite once its requested
        /// </summary>
        public Sprite MoveX {
            get {
                if (_moveX == null)
                    LoadSpriteX();
                return _moveX;
            }
            private set => _moveX = value;
        }

        public Sprite MoveO {
            get {
                if (_moveO == null)
                    LoadSpriteO();
                return _moveO;
            }
            private set => _moveO = value;
        }

        /// <summary>
        /// Release the loaded sprites when done
        /// </summary>
        protected void OnDestroy() {
            if (_spriteXOperation.IsValid())
                Addressables.Release(_spriteXOperation);
            if (_spriteOOperation.IsValid())
                Addressables.Release(_spriteOOperation);
        }

        public Sprite GetSprite(GameConventions.Moves move) {
            if (move is GameConventions.Moves.X)
                return MoveX;
            if (move is GameConventions.Moves.O)
                return MoveO;
            return null;
        }

        private void LoadSpriteO() {
            _spriteOOperation = _moveORef.LoadAssetAsync<Sprite>();
            _spriteOOperation.Completed += operation => {
                if (!CheckOperation(operation))
                    return;
                MoveO = operation.Result;
            };
        }

        private void LoadSpriteX() {
            _spriteXOperation = _moveXRef.LoadAssetAsync<Sprite>();
            _spriteXOperation.Completed += operation => {
                if (!CheckOperation(operation))
                    return;
                MoveX = operation.Result;
            };
        }

        private bool CheckOperation(AsyncOperationHandle<Sprite> operation) {
            switch (operation.Status) {
                case AsyncOperationStatus.Succeeded:
                    return true;
                case AsyncOperationStatus.Failed:
                    Debug.LogError("Failed to load Sprite!");
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}