using System.Collections.Generic;
using static Core.GameConventions;

namespace Core.Player.Types {
    public class PlayerType {

        public delegate int NextMove(Moves[,] board, ICollection<int> playerMoves, ICollection<int> otherPlayerMoves);
        
        private readonly NextMove _nextMove;
        private readonly bool _usesInput;

        public PlayerType(bool usesInput, NextMove nextMove) {
            _usesInput = usesInput;
            _nextMove = nextMove;
        }

        public bool GetUsesInput() {
            return _usesInput;
        }

        public NextMove GetNextMove() {
            return _nextMove;
        }
    }
}