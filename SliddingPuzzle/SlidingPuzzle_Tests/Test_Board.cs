using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_Board
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Board board = new Board();
            Assert.AreNotEqual(null,board.Positions);
            Assert.AreEqual(0,board.RowSize);
            Assert.AreEqual(0, board.ColumnSize);
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            Board board = new Board(3,3);
            Assert.AreNotEqual(null, board.Positions);
            Assert.AreEqual(3, board.RowSize);
            Assert.AreEqual(3, board.ColumnSize);
        }
    }
}
