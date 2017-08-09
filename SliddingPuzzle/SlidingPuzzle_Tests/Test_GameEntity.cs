using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.ADO;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_GameEntity
    {
        [TestMethod]
        public void Test_Constructor()
        {
            GameEntity game = new GameEntity();
            Assert.AreEqual(null,game.HashedGame);
            Assert.AreEqual(null, game.CurrentGame);
            Assert.AreEqual(null, game.Solution);
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            GameEntity game = new GameEntity("Test","Test","Test");
            Assert.AreEqual("Test", game.HashedGame);
            Assert.AreEqual("Test", game.CurrentGame);
            Assert.AreEqual("Test", game.Solution);
        }
    }
}
