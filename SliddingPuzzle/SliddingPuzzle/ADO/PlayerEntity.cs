using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace SliddingPuzzle.ADO
{
   public class PlayerEntity:TableEntity
    {
        public PlayerEntity(string username, string pwd)
        {
            this.PartitionKey = username;
            this.RowKey = pwd;
            Username = username;
            Pwd = pwd;
            BlobUrl = String.Empty;
        }

        public PlayerEntity() { }

        public string Username { get; set; }

        public string Pwd { get; set; }

        public string BlobUrl { get; set; }
    }
}
