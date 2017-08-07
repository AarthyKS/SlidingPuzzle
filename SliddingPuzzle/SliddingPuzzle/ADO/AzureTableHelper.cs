using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SliddingPuzzle.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
// Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace SliddingPuzzle.ADO
{
    public class AzureTableHelper
    {
        private static StorageCredentials cred = new StorageCredentials("puzzle", "f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==");

        public static async void CreateTable(string name = "player")
        {
            CloudStorageAccount storageAccount =
              CloudStorageAccount.Parse(
                  "DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(name);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();
        }

        public static async Task<bool> Insert(Player player)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("player");

            PlayerEntity player1 = new PlayerEntity(player.Username, player.HashedPassword);
            

            bool ifExists = true;

            TableOperation operation = TableOperation.Retrieve(player.Username, player.HashedPassword);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
                ifExists = true;
            }
            else
            {
                ifExists = false;
            }

            if (!ifExists)
            {
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(player1);

                // Execute the insert operation.
                var result = table.ExecuteAsync(insertOperation).Result;
                if (result.Result == null)
                {
                    ifExists = true;
                }
            }

            return !ifExists;

        }

        public static async Task<bool> Insert(Game game)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("game");

            GameEntity game1 = new GameEntity(game.CurrentGame, game.HashedGame,game.Solution);


            bool ifExists = true;

            TableOperation operation = TableOperation.Retrieve(game.HashedGame, game.HashedGame);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
                ifExists = true;
            }
            else
            {
                ifExists = false;
            }

            if (!ifExists)
            {
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(game1);

                // Execute the insert operation.
                var result = table.ExecuteAsync(insertOperation).Result;
                if (result.Result == null)
                {
                    ifExists = true;
                }
            }

            return !ifExists;

        }

        public static async Task<bool> Validate(Player player)
        {
            // Retrieve the storage account from the connection string.
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("player");

            TableOperation operation = TableOperation.Retrieve(player.Username,player.HashedPassword);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
               return true;
            }
            return false;

        }

        public static async Task<Player> GetPlayer(Player player)
        {
            // Retrieve the storage account from the connection string.
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("player");

            TableOperation operation = TableOperation.Retrieve(player.Username, player.HashedPassword);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
                player.SavedGameId = ((DynamicTableEntity) res.Result).Properties["SavedGameId"].StringValue;
                return player;
            }
            return null;

        }

        public static async Task<Player> Update(Player player)
        {
            // Retrieve the storage account from the connection string.
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("player");

            PlayerEntity player1 = new PlayerEntity(player.Username, player.HashedPassword);
            player1.SavedGameId = player.SavedGameId;

            TableOperation operation = TableOperation.InsertOrMerge(player1);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
                return player;
            }
            return null;

        }

        public static async Task<Game> GetGame(Game game)
        {
            // Retrieve the storage account from the connection string.
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=puzzle;AccountKey=f9QhQ2aU6zEGOAI9i4Ek6iJEgwRgYhFCyXtNmtCTO4R4uXwylqbYfaeN/Muyk7tlV3/Z5nFSIzadlLnh+Tt/rQ==;EndpointSuffix=core.windows.net");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("game");

            TableOperation operation = TableOperation.Retrieve(game.HashedGame, game.HashedGame);

            var res = table.ExecuteAsync(operation).Result;

            if (res.Result != null)
            {
                game.CurrentGame = ((DynamicTableEntity)res.Result).Properties["CurrentGame"].StringValue;
                game.Solution = ((DynamicTableEntity) res.Result).Properties["Solution"].StringValue;
                return game;
            }
            return null;

        }
    }
}
