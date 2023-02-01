using System.Collections.Generic;
using Core.Extensions.Board;
using Core.Extensions.Random;
using Core.Player.Scripts;
using Core.Player.Types;
using static Core.GameConventions;

namespace Core.Player.Factory {
    /// <summary>
    /// Creates a player that has random moves generated
    /// </summary>
    public class PlayerComputerFactory : PlayerFactoryBase<GamePlayer> {
        public override GamePlayer CreatePlayer() {
            return new GamePlayer(CreateType());
        }

        protected override PlayerType CreateType() {
            return new PlayerType(false, GetNextMove);
        }

        protected override int GetNextMove(Moves[,] board, ICollection<int> playerMoves,
            ICollection<int> otherPlayerMoves) {
            return GetRandomMove(board, playerMoves, otherPlayerMoves);
        }

        /// <summary>
        /// Returns a valid random move that fits on the board
        /// </summary>
        /// <returns></returns>
        private int GetRandomMove(Moves[,] board, ICollection<int> playerMoves,
            ICollection<int> otherPlayerMoves) {
            var move = DefaultMoveIndex;
            var tries = 0;
            while (!board.CheckForValidPlacement(move)) {
                if (tries > BoardLength)
                    return DefaultMoveIndex;
                move = GetRandomUnPlayedMove(playerMoves, otherPlayerMoves);
                tries++;
            }

            return move;
        }

        /// <summary>
        /// Gets a random unPlayed move by checking the played moves
        /// </summary>
        /// <returns></returns>
        private int GetRandomUnPlayedMove(ICollection<int> playerMoves, ICollection<int> otherPlayerMoves) {
            int randomMove;
            var tries = 0;
            do {
                if (tries > MaxTries)
                    return DefaultMoveIndex;
                tries++;
                randomMove = RandomExtensions.GetRandomNumber(0, BoardLength);
            } while (playerMoves.Contains(randomMove) || otherPlayerMoves.Contains(randomMove));

            return randomMove;
        }
    }
}