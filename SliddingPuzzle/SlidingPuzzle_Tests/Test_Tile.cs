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

        [TestMethod]
        public void Test_Constructor2()
        {
            Tile tile = new Tile("test","test",1,1);
            Assert.AreEqual("test", tile.Id);
            Assert.AreEqual(1, tile.X);
            Assert.AreEqual(1, tile.Y);
            Assert.AreEqual("test", tile.Value);
        }
    }
}
