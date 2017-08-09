using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.ADO;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_PlayerEntity
    {
        [TestMethod]
        public void Test_Constructor()
        {
            PlayerEntity player = new PlayerEntity();
            Assert.AreEqual(null,player.Username);
            Assert.AreEqual(null, player.HashedPassword);
            Assert.AreEqual(null, player.SavedGameId);
        }

        [TestMethod]
        public void Test_Constructor2()
        {
            string hash = Player.HashMsg("Test");
            PlayerEntity player = new PlayerEntity("test", hash);

            Assert.AreEqual("test", player.Username);
            Assert.AreEqual(hash,player.HashedPassword);
            Assert.AreEqual(player.Username,player.PartitionKey);
            Assert.AreEqual(player.HashedPassword,player.RowKey);
        }
    }
}
