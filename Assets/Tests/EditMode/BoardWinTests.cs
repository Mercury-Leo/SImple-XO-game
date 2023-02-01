using System.Collections.Generic;
using NUnit.Framework;
using Tests.EditMode.Base;

namespace Tests.EditMode {
    /// <summary>
    /// Checks the winning states of the player
    /// </summary>
    public class BoardWinTests : BoardTestsBase {
        [TestCaseSource(nameof(WinCases))]
        public void PlayerWins(List<int> options) {
            Assert.IsTrue(CheckBoardTypeAllOptions(options).won);
        }
    }
}