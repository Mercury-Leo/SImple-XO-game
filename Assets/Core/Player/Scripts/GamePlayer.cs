using System.Collections.Generic;
using Core.Extensions.Board;
using static Core.GameConventions;

namespace Core.Player.Scripts {
    public class GamePlayer : IPlayer {
        private readonly HashSet<int> _playerMoves = new HashSet<int>();
        private readonly HashSet<int> _otherPlayerMoves = new HashSet<int>();
        
        private readonly Types.PlayerType _type;

        public Moves Move { get; set; }
        public BoardStates CurrentState { get; set; } = BoardStates.Playing;
        public string Name { get; set; }

        private Moves[,] Board { get; set; } = new Moves[BoardSize, BoardSize];

        public GamePlayer(Types.PlayerType type) {
            _type = type;
        }

        public bool UsesInput() {
            return _type.GetUsesInput();
        }

        /// <summary>
        /// When a move is performed update the player on that move
        /// </summary>
        /// <param name="index"></param>
        /// <param name="move"></param>
        public void PerformMove(int index, Moves move) {
            if (GuardType())
                return;

            Board.InsertToBoard(index, move);

            if (move == Move)
                _playerMoves.Add(index);
            if (move != Move)
                _otherPlayerMoves.Add(index);
        }

        public int GetNextMove() {
            if (GuardType())
                return DefaultMoveIndex;

            return _type.GetNextMove().Invoke(Board, _playerMoves, _otherPlayerMoves);
        }

        public void ResetPlayer() {
            _playerMoves.Clear();
            _otherPlayerMoves.Clear();
            Board = new Moves[BoardSize, BoardSize];
        }

        public void EndGame(BoardStates state) {
            CurrentState = state;
        }

        private bool GuardType() {
            return _type is null;
        }
    }
}