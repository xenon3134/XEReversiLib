using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XEReversiLib.Core;

namespace XEReversiLibTest.Core
{
    [TestClass]
    public class InitRuleTest
    {
        [TestMethod]
        public void TestNormalInitRule()
        {
            Board board = Board.CreateStandard();
            NormalInitRule.Standard.Initialize(board);

            Assert.AreEqual(StandardCellStatus.PLAYER1, board.GetCell(Space.D2.GetPosition(4, 4)).Status);
            Assert.AreEqual(StandardCellStatus.PLAYER2, board.GetCell(Space.D2.GetPosition(4, 5)).Status);
            Assert.AreEqual(StandardCellStatus.PLAYER2, board.GetCell(Space.D2.GetPosition(5, 4)).Status);
            Assert.AreEqual(StandardCellStatus.PLAYER1, board.GetCell(Space.D2.GetPosition(5, 5)).Status);
        }
    }
}
