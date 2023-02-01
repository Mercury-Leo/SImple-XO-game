using System.Collections.Generic;
using Core;
using Core.Extensions.Board;
using NUnit.Framework;

namespace Tests.EditMode.Base {
    public class BoardTestsBase {
        protected GameConventions.Moves[,] MockBoard;
        protected const GameConventions.Moves Move = GameConventions.Moves.X;
        protected const GameConventions.Moves OtherMove = GameConventions.Moves.O;
        protected const int BoardLength = GameConventions.BoardSize * GameConventions.BoardSize;

        protected static object[] WinCases = {
            // Diagonal
            new object[] { new List<int> { 0, 4, 8 } },
            // Anti diagonal
            new object[] { new List<int> { 2, 4, 6 } },
            // Columns
            new object[] { new List<int> { 0, 3, 6 } },
            new object[] { new List<int> { 1, 4, 7 } },
            new object[] { new List<int> { 2, 5, 8 } },
            // Rows
            new object[] { new List<int> { 0, 1, 2 } },
            new object[] { new List<int> { 3, 4, 5 } },
            new object[] { new List<int> { 6, 7, 8 } },
        };

        private void InitializeBoard() {
            MockBoard = new GameConventions.Moves[GameConventions.BoardSize, GameConventions.BoardSize];
        }

        protected (bool won, GameConventions.Moves move) CheckBoardTypeAllOptions(List<int> options) {
            MockBoard.InsertToBoard(options[0], Move);
            MockBoard.InsertToBoard(options[1], Move);
            var coords = MockBoard.InsertToBoard(options[2], Move);
            return MockBoard.CheckForWinningState(coords.x, coords.y, Move);
        }
        
        [SetUp]
        public virtual void SetUpTest() {
            InitializeBoard();
        }
    }
}