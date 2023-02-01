using System.Collections.Generic;
using Core.Extensions.Board;
using NUnit.Framework;
using Tests.EditMode.Base;

namespace Tests.EditMode {
    public class BoardDrawTests : BoardTestsBase {
        protected static object[] MoveIndexes = {
            new object[] { new List<int> { 0, 2, 3, 7, 8 }, new List<int> { 1, 4, 5, 6 } },
        };

        [TestCaseSource(nameof(MoveIndexes))]
        public void CheckGameDrawByDecision(List<int> moves, List<int> otherMoves) {
            foreach (var index in moves) {
                MockBoard.InsertToBoard(index, Move);
            }

            foreach (var index in otherMoves) {
                MockBoard.InsertToBoard(index, OtherMove);
            }

            var canWin = MockBoard.CheckForWinningState(0, 0, Move).won;
            Assert.IsFalse(canWin);
        }
    }
}