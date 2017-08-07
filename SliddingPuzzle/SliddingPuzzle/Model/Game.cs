using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Game:IGame
    {
        public string CurrentGame { get; set; }

        public string HashedGame { get; set; }

        public string Solution { get; set; }
    }
}
