using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_Game
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Game game = new Game();
            Assert.AreEqual(null,game.HashedGame);
            Assert.AreEqual(null,game.CurrentGame);
            Assert.AreEqual(null,game.Solution);
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            Game game = new Game()
            {
                CurrentGame = "test",
                HashedGame = "test",
                Solution = "test"
            };
            Assert.AreEqual("test", game.HashedGame);
            Assert.AreEqual("test", game.CurrentGame);
            Assert.AreEqual("test", game.Solution);
        }
    }
}
