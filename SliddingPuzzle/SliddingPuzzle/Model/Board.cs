using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Board
    { 

        public Tile[,] Positions { get; set; }

        public Board()
        {
            Positions = new Tile[3,3];
        }

        public Board(int rowSize, int columnSize)
        {
            Positions = new Tile[rowSize, columnSize];
        }
    }
}
