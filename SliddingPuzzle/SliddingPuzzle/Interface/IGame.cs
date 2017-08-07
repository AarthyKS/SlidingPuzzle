using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle
{
    interface IGame
    {
        string CurrentGame { get; set; }

        string HashedGame { get; set; }

        string Solution { get; set; }
    }
}
