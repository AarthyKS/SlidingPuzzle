using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_Puzzle
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Puzzle puzzle = new Puzzle();
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            Puzzle puzzle = new Puzzle(4,4);
            Assert.AreEqual(4,puzzle.PuzzleBoard.ColumnSize);
            Assert.AreEqual(4, puzzle.PuzzleBoard.RowSize);
        }

        [TestMethod]
        public void Test_GenerateNumbericBoard()
        {
            Puzzle puzzle = new Puzzle(3, 3);
            Assert.AreEqual(3, puzzle.PuzzleBoard.ColumnSize);
            Assert.AreEqual(3, puzzle.PuzzleBoard.RowSize);
            Assert.IsTrue(puzzle.PuzzleBoard!=null);
            Assert.IsTrue(puzzle.PuzzleBoard.Positions.Count == 0);
            puzzle.GenerateNumericBoard();

            Assert.IsTrue(puzzle.PuzzleBoard.Positions.Count != 0);
            Assert.IsTrue(puzzle.PuzzleBoard.Positions.Count == 9);

            foreach (var tile in puzzle.PuzzleBoard.Positions)
            {
                Assert.IsTrue(!String.IsNullOrEmpty(tile.Id));
            }

        }

        [TestMethod]
        public void Test_MixPuzzle()
        {
            Puzzle puzzle = new Puzzle(3, 3);
            Assert.AreEqual(3, puzzle.PuzzleBoard.ColumnSize);
            Assert.AreEqual(3, puzzle.PuzzleBoard.RowSize);
            Assert.IsTrue(puzzle.PuzzleBoard != null);
            Assert.IsTrue(puzzle.PuzzleBoard.Positions.Count == 0);
            puzzle.GenerateNumericBoard();

            List<Tile> original = puzzle.PuzzleBoard.Positions;

            puzzle.MixPuzzle();

            for (int i = 0; i < puzzle.PuzzleBoard.Positions.Count; i++)
            {
                
            }
        }

        [TestMethod]
        public void Test_CheckMoveValidity()
        {
            Puzzle puzzle = new Puzzle(3, 3);
            puzzle.GenerateNumericBoard();

            List<Tile> original = puzzle.PuzzleBoard.Positions;

            Assert.IsTrue(puzzle.CheckMoveValidity(2, 3));

            Assert.IsFalse(puzzle.CheckMoveValidity(1, 1));
        }
    }
}
