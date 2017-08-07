using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle
{
    interface IPlayer
    {
        string Username { get; set; }

        string HashedPassword { get; }

       string SavedGameId { get; set; }
    }
}
