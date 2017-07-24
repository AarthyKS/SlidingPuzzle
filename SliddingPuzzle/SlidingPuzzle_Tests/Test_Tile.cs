using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_Tile
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Tile tile = new Tile();
            Assert.AreEqual(null,tile.Id);
            Assert.AreEqual(0,tile.X);
            Assert.AreEqual(0,tile.Y);
            Assert.AreEqual(null,tile.Value);
        }
    }
}
