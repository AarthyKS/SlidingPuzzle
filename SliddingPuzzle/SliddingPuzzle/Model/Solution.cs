using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Solution
    {
        public List<Tile> InitialPositions { get; set; }
        public List<KeyValuePair<int,int>> Moves { get; set; } 
    }
}
