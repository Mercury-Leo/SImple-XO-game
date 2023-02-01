using UnityEngine;
using static Core.GameConventions;

namespace Core.Extensions.Board {
    public static class BoardExtensions {
        public static readonly (int, int) InvalidCoords = (-1, -1);

        /// <summary>
        /// Converts the tile index into a 2D board coords
        /// <example>
        /// Index = 1 will be converted to (x,y) = (0,1)
        /// </example>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        public static (int x, int y) ConvertToBoardCoords(int index, int boardSize) {
            if (boardSize <= 2)
                return InvalidCoords;
            return index < 0 ? InvalidCoords : (index / boardSize, index % boardSize);
        }

        public static bool CheckForValidPlacement(this Moves[,] board, int index) {
            if (GuardBoard(board, index))
                return false; 

            var coords = ConvertToBoardCoords(index, BoardSize);

            if (GuardCoords(coords))
                return false;

            var x = coords.x;
            var y = coords.y;

            return board[x, y] is Moves.Empty;
        }

        public static (int x, int y) InsertToBoard(this Moves[,] board, int index, Moves move) {
            if (GuardBoard(board, index))
                return InvalidCoords;

            var coords = ConvertToBoardCoords(index, BoardSize);

            if (GuardCoords(coords))
                return InvalidCoords;

            if (!board.CheckForValidPlacement(index)) return InvalidCoords;

            board[coords.x, coords.y] = move;
            return coords;
        }

        public static void UndoMove(this Moves[,] board, int index) {
            if (GuardBoard(board, index))
                return;

            var coords = ConvertToBoardCoords(index, BoardSize);

            if (GuardCoords(coords))
                return;

            board[coords.x, coords.y] = Moves.Empty;
        }

        public static (bool won, Moves move) CheckForWinningState(this Moves[,] board, int x, int y, Moves move) {
            if (RowsCheck(board, x, move).won)
                return (true, move);

            if (ColumnCheck(board, y, move).won)
                return (true, move);

            if (DiagonalCheck(board, move).won)
                return (true, move);

            if (AntiDiagonalCheck(board, move).won)
                return (true, move);

            return (false, move);
        }

        private static (bool won, Moves move) RowsCheck(Moves[,] board, int x, Moves move) {
            for (var i = 0; i < BoardSize; i++) {
                if (board[x, i] != move)
                    break;

                if (i != BoardSize - 1) continue;

                return (true, move);
            }

            return (false, move);
        }

        private static (bool won, Moves move) ColumnCheck(Moves[,] board, int y, Moves move) {
            for (var i = 0; i < BoardSize; i++) {
                if (board[i, y] != move)
                    break;

                if (i != BoardSize - 1) continue;

                return (true, move);
            }

            return (false, move);
        }

        private static (bool won, Moves move) DiagonalCheck(Moves[,] board, Moves move) {
            for (var i = 0; i < BoardSize; i++) {
                if (board[i, i] != move)
                    break;

                if (i != BoardSize - 1) continue;

                return (true, move);
            }

            return (false, move);
        }

        private static (bool won, Moves move) AntiDiagonalCheck(Moves[,] board, Moves move) {
            var columnIndex = BoardSize - 1;
            for (var i = 0; i < BoardSize; i++) {
                if (board[i, columnIndex--] != move)
                    break;

                if (i != BoardSize - 1) continue;

                return (true, move);
            }

            return (false, move);
        }

        private static bool GuardCoords((int, int) coords) {
            if (coords != InvalidCoords) return false;

            Debug.LogWarning("Board coords received are invalid.");
            return true;
        }

        private static bool GuardBoard(Moves[,] board, int index) {
            if (board is null)
                return true;
            if (index < 0)
                return true;
            return false;
        }
    }
}