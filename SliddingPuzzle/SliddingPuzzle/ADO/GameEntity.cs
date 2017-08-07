using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Microsoft.WindowsAzure.Storage.Table;

namespace SliddingPuzzle.ADO
{
    public class GameEntity:TableEntity,IGame
    {
        public GameEntity(string game, string hashedGame,string solution)
        {
            this.PartitionKey = hashedGame;
            this.RowKey = hashedGame;
            CurrentGame = game;
            HashedGame = hashedGame;
            Solution = solution;
        }

        public GameEntity() { }

        public string CurrentGame { get; set; }

        public string HashedGame { get; set; }

        public string Solution { get; set; }
    }
}
