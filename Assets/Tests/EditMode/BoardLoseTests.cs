using System.Collections.Generic;
using NUnit.Framework;
using Tests.EditMode.Base;

namespace Tests.EditMode {
    /// <summary>
    /// These tests check for the losing player states
    /// As the last move of the game indicates whether it's a win or a lose
    /// We know that if player X won, player O must have lost
    /// </summary>
    public class BoardLoseTests : BoardTestsBase {
        [TestCaseSource(nameof(WinCases))]
        public void PlayerLoses(List<int> options) {
            var check = CheckBoardTypeAllOptions(options);

            Assert.AreNotEqual(OtherMove, check.move);
        }
    }
}