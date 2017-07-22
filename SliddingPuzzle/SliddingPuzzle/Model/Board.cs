using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Board
    { 

        public List<Tile> Positions { get; set; }

        public int RowSize { get; set; }

        public int ColumnSize { get; set; }


        public Board()
        {
            Positions = new List<Tile>();
        }

        public Board(int rowSize,int columnSize)
        {
            Positions = new List<Tile>();
            RowSize = rowSize;
            ColumnSize = columnSize;
        }
    }
}
