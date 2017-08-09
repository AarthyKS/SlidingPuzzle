using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SliddingPuzzle.ADO;
using SliddingPuzzle.Model;

namespace SlidingPuzzle_Tests
{
    [TestClass]
    public class Test_AzureHelper
    {
        [TestMethod]
        public void Test_Constructor()
        {
            try
            {
                AzureTableHelper helper = new AzureTableHelper();
            }
            catch (Exception)
            {

             Assert.Fail("Constructor failed");
            }
        }

        [TestMethod]
        public void Test_CreateTable()
        {
            Create();
        }

        private async void Create()
        {
            try
            {
                AzureTableHelper.CreateTable("test"+DateTime.Now.ToString("MMddyyHHmmss"));
                await Task.Delay(TimeSpan.FromSeconds(45));
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_Validate()
        {
           Validate();
        }

        private async void Validate()
        {
            try
            {
                Player player = new Player
                {
                    Username = "test",
                    Password = "testing"
                };
                var res = AzureTableHelper.Validate(player).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_Insert()
        {
           Insert();
        }

        private async void Insert()
        {
            try
            {
                Player player = new Player
                {
                    Username = DateTime.Now.ToString("MMddyyHHmmss"),
                    Password = "testing"
                };
                var res = AzureTableHelper.Insert(player).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_InsertGame()
        {
            InsertGetGame();
        }

        private async void InsertGetGame()
        {
            try
            {
                Game game = new Game();
                game.CurrentGame = DateTime.Now.ToString();
                game.Solution = DateTime.Now.ToString();
                game.HashedGame = DateTime.Now.ToString().GetHashCode().ToString();

                var res = AzureTableHelper.Insert(game).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));

                var res2 = AzureTableHelper.GetGame(game).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void Test_GetPlayer()
        {
            GetUpdatePlayer();
        }

        private async void GetUpdatePlayer()
        {
            try
            {
                Player player = new Player
                {
                    Username = DateTime.Now.ToString("MMddyyHHmmss"),
                    Password = "testing"
                };
                var res = AzureTableHelper.GetPlayer(player).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));

                player.Password = "testing1";

                res = AzureTableHelper.Update(player).Result;
                await Task.Delay(TimeSpan.FromSeconds(45));

            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

    }
}
