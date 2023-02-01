using System;
using System.Collections.Generic;
using Core.Player.Scripts;
using Core.SpriteDataBase.Scripts;
using UnityEngine;
using static Core.GameConventions;

namespace Core.Game.Board.Scripts {
    public class BoardManager : MonoBehaviour {
        [SerializeField] private GameObject _content;

        [Header("DataBase")] 
        [SerializeField] private SpriteDataBaseSO _spriteDataBase;

        private readonly List<Tile> _tiles = new List<Tile>();
        private readonly Stack<(IPlayer player, int index)> _moves = new Stack<(IPlayer player, int index)>();
        private readonly BoardLogic _boardLogic = new BoardLogic(BoardSize);

        public Action<BoardStates> OnGameOver { get; set; }
        public Action<int> OnTileSelected { get; set; }
        public Action<int, Moves> OnMoveMade { get; set; }

        private void Awake() {
            InitializeTiles();
        }

        private void OnEnable() {
            _boardLogic.OnGameDecision += GameDecided;
        }

        private void OnDisable() {
            _boardLogic.OnGameDecision -= GameDecided;
        }

        /// <summary>
        /// Sets a tile sprite and performs a move on the board
        /// </summary>
        /// <param name="index">Index to perform move on</param>
        /// <param name="activePlayer"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetTile(int index, IPlayer activePlayer) {
            var moveSprite = _spriteDataBase.GetSprite(activePlayer.Move);
            _tiles[index].SetSprite(moveSprite);

            _moves.Push((activePlayer, index));
            _boardLogic.PerformMove(index, activePlayer.Move);
            OnMoveMade?.Invoke(index, activePlayer.Move);
        }

        public void UndoLastRound() {
            UndoMove();
            UndoMove();
        }

        private void UndoMove() {
            if (_moves.Count <= 0)
                return;

            var move = _moves.Pop();
            var tile = _tiles[move.index];
            if (tile is null)
                return;
            tile.ClearTile();
            _boardLogic.UndoMove(move.index);
        }

        public void ResetBoard() {
            _moves.Clear();
            _boardLogic.ClearBoard();
            ResetTiles();
        }

        private void GameDecided(BoardStates boardState, Moves move) {
            OnGameOver?.Invoke(boardState);
        }

        private void InitializeTiles() {
            for (var i = 0; i < _content.transform.childCount; i++) {
                var tile = _content.transform.GetChild(i).GetComponent<Tile>();
                if (tile is null)
                    return;

                tile.OnClick += OnTileClicked;
                _tiles.Add(tile);
            }
        }

        /// <summary>
        /// Called only when an empty tile is clicked
        /// </summary>
        /// <param name="index"></param>
        private void OnTileClicked(int index) {
            OnTileSelected?.Invoke(index);
        }

        private void ResetTiles() {
            foreach (var tile in _tiles) {
                tile.ClearTile();
            }
        }
    }
}