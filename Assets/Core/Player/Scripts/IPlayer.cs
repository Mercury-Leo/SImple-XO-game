namespace Core.Player.Scripts {
    /// <summary>
    /// The Player interface dictates the functions and properties a player should have
    /// Any player needs to implement this interface to work with the game
    /// </summary>
    public interface IPlayer {
        
        /// <summary>
        /// The assigned move of the player
        /// </summary>
        public GameConventions.Moves Move { get; set; }

        /// <summary>
        /// The state of the player, have they won, lost or in a draw
        /// </summary>
        public GameConventions.BoardStates CurrentState { get; set; }
        
        public string Name { get; set; }

        public bool UsesInput();

        /// <summary>
        /// When a move is performed update the player on that move
        /// </summary>
        /// <param name="index"></param>
        /// <param name="move"></param>
        public abstract void PerformMove(int index, GameConventions.Moves move);

        /// <summary>
        /// Gets the next move of the player, used by non input players
        /// </summary>
        /// <returns></returns>
        public abstract int GetNextMove();

        /// <summary>
        /// Resets the player's data
        /// </summary>
        public abstract void ResetPlayer();

        public abstract void EndGame(GameConventions.BoardStates state);
    }
}