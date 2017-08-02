using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace SliddingPuzzle.ADO
{
   public class PlayerEntity:TableEntity,IPlayer
    {
        public PlayerEntity(string username, string pwd)
        {
            this.PartitionKey = username;
            this.RowKey = pwd;
            Username = username;
            HashedPassword = pwd;
            BlobUrl = String.Empty;
        }

        public PlayerEntity() { }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string BlobUrl { get; set; }
    }
}
