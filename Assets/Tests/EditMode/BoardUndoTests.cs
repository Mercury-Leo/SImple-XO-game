using System.Collections.Generic;
using Core;
using Core.Extensions.Board;
using NUnit.Framework;
using Tests.EditMode.Base;

namespace Tests.EditMode {
    public class BoardUndoTests : BoardTestsBase {
        private readonly Stack<(GameConventions.Moves move, int index)> _moves =
            new Stack<(GameConventions.Moves move, int index)>();

        [Test, Sequential]
        public void PlayerUndoTurn([Range(0, BoardLength - 1)] int index) {
            MockBoard.InsertToBoard(index, Move);

            MockBoard.UndoMove(index);
            var coords = BoardExtensions.ConvertToBoardCoords(index, GameConventions.BoardSize);
            Assert.AreEqual(MockBoard[coords.x, coords.y], GameConventions.Moves.Empty);
        }

        [SetUp]
        public override void SetUpTest() {
            base.SetUpTest();
            _moves.Clear();
        }
    }
}