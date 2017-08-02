using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_Player
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Player player = new Player
            {
                Username = "Test",
                Password = "Test",
                BlobUrl = "https://test.com"
            };
            Assert.IsTrue(!String.IsNullOrEmpty(player.HashedPassword));
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            Player player = new Player
            {
                Username = "Test",
                Password = "Test",
                BlobUrl = "https://test.com"
            };
            string hash = Player.HashMsg("Test");
            Assert.AreEqual(hash,player.HashedPassword);
        }

        [TestMethod]
        public void Test_Hash1()
        {
            string hash_1 = Player.HashMsg("Test");
            string hash_2 = Player.HashMsg("test");

            Assert.AreNotEqual(hash_1,hash_2);
        }
    }
}
