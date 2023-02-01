using System.Collections.Generic;
using Core.Player.Scripts;
using Core.Player.Types;
using static Core.GameConventions;
namespace Core.Player.Factory {
    /// <summary>
    /// Base factory for creating players
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PlayerFactoryBase<T> : IPlayerFactory<T> where T : IPlayer{
        public abstract T CreatePlayer();

        public T GetPlayer() {
            return CreatePlayer();
        }

        protected abstract PlayerType CreateType();
        
        /// <summary>
        /// Next move will return a move for the player to play
        /// In cases it doesn't need work it will return <see cref="DefaultMoveIndex"/>
        /// Any implementation for computer type or remote users should use this function 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerMoves"></param>
        /// <param name="otherPlayerMoves"></param>
        /// <returns></returns>
        protected abstract int GetNextMove(Moves[,] board, ICollection<int> playerMoves,
            ICollection<int> otherPlayerMoves);
    }
}
