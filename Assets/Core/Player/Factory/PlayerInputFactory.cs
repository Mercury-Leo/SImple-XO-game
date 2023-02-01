using System.Collections.Generic;
using Core.Player.Types;
using static Core.GameConventions;

namespace Core.Player.Factory {
    /// <summary>
    /// Creates a player that uses Input (mouse)
    /// </summary>
    public class PlayerInputFactory : PlayerFactoryBase<Scripts.GamePlayer> {
        public override Scripts.GamePlayer CreatePlayer() {
            return new Scripts.GamePlayer(CreateType());
        }

        protected override PlayerType CreateType() {
            return new PlayerType(true, GetNextMove);
        }
        
        protected override int GetNextMove(Moves[,] board, ICollection<int> playerMoves,
            ICollection<int> otherPlayerMoves) {
            return DefaultMoveIndex;
        }
    }
}