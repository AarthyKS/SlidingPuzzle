using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Puzzle
    {
        #region Members
        /// <summary>
        /// Puzzle board visualization object
        /// </summary>
        public Board PuzzleBoard { get; set; }

        /// <summary>
        /// Position of the empty tile (row and column index)
        /// </summary>
        public KeyValuePair<int, int> EmptyTilePosition { get; set; }

        /// <summary>
        /// Position of currently selected tile (row and column index) 
        /// </summary>
        public  KeyValuePair<int,int> CurrentTilePostion { get; set; }

        #endregion

        #region Constructor

        public Puzzle()
        {
        }

        public Puzzle(int totalRows, int totalColumn)
        {
            PuzzleBoard = new Board(totalRows,totalColumn);
        }

        #endregion

        #region Methods

        public void GenerateNumericBoard()
        {
            int count = 1;
            for (int i = 0; i < PuzzleBoard.Positions.GetLength(0); i++)
            {
                for(int j = 0; j < PuzzleBoard.Positions.GetLength(1);j++)
                {
                    Tile myTile = new Tile(count.ToString(), count.ToString());
                    PuzzleBoard.Positions[i,j] = myTile;
                }
            }

        }

        #endregion
    }
}
