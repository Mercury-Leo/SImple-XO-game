using System;
using Core.Extensions.Board;
using static Core.GameConventions;

namespace Core.Game.Board.Scripts {
    /// <summary>
    /// Based on the logic from <see cref="https://stackoverflow.com/questions/1056316/algorithm-for-determining-tic-tac-toe-game-over"/>
    /// Board logic should be humble as possible and no interaction with any Monobehaviours
    /// Holds a board and handles all the board logic:
    /// Clearing the board
    /// Undo a move
    /// Perform a move
    /// </summary>
    public class BoardLogic {
        private readonly int _maxMoves;
        private readonly int _boardSize;
        private int _moveCount;

        public Moves[,] Board { get; private set; }

        public Action<BoardStates, Moves> OnGameDecision { get; set; }

        public BoardLogic(int boardSize, Moves[,] board = null) {
            _boardSize = boardSize;
            _maxMoves = (int)(Math.Pow(_boardSize, 2) - 1);
            Board = board ?? new Moves[_boardSize, _boardSize];
        }

        public void ClearBoard() {
            _moveCount = 0;
            Board = new Moves[_boardSize, _boardSize];
        }

        public void UndoMove(int index) {
            Board.UndoMove(index);
            _moveCount--;
        }

        /// <summary>
        /// Performs a <see cref="move"/> on the board at <see cref="index"/>
        /// </summary>
        /// <param name="index">Index to place</param>
        /// <param name="move">Type of move</param>
        public void PerformMove(int index, Moves move) {
            if (_moveCount > _maxMoves)
                return;
            
            var coords = Board.InsertToBoard(index, move);

            if (coords == BoardExtensions.InvalidCoords)
                return;

            _moveCount++;

            if (Board.CheckForWinningState(coords.x, coords.y, move).won) {
                OnGameDecision?.Invoke(BoardStates.Win, move);
                return;
            }

            if (_moveCount > _maxMoves)
                OnGameDecision?.Invoke(BoardStates.Draw, move);
        }
    }
}