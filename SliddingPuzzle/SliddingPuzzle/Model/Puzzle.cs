using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace SliddingPuzzle.Model
{
    public class Puzzle
    {
        #region Members

        /// <summary>
        /// Puzzle board visualization object
        /// </summary>
        public Board PuzzleBoard { get; set; }

        public Solution PuzzleSolution { get; set; }

        /// <summary>
        /// Position of the empty tile (row and column index)
        /// </summary>
        public KeyValuePair<int, int> EmptyTilePosition { get; set; }

        /// <summary>
        /// Position of currently selected tile (row and column index) 
        /// </summary>
        public KeyValuePair<int, int> CurrentTilePostion { get; set; }

        #endregion

        #region Constructor

        public Puzzle()
        {
        }

        public Puzzle(int totalRows, int totalColumn)
        {
            PuzzleBoard = new Board(totalRows, totalColumn);
            PuzzleBoard.Positions = new List<Tile>();
            PuzzleSolution = new Solution();
        }

        #endregion

        #region Methods

        public void GenerateNumericBoard()
        {
            int count = 1;
            for (int i = 0; i < PuzzleBoard.RowSize; i++)
            {
                for (int j = 0; j < PuzzleBoard.ColumnSize; j++)
                {
                    Tile myTile = new Tile(count.ToString(), count.ToString(), i, j);
                    if (count == PuzzleBoard.RowSize*PuzzleBoard.ColumnSize)
                    {
                        myTile.Value = "";
                        EmptyTilePosition = new KeyValuePair<int, int>(i, j);
                    }
                    PuzzleBoard.Positions.Add(myTile);
                    count++;
                }
            }
        }

        public void MixPuzzle()
        {
            PuzzleSolution.Moves = new List<KeyValuePair<int, int>>();
            int rand = new Random().Next(0, 1);
            int rand2 = new Random().Next(0,1);
            for (int i = 0; i < new Random().Next(5,35); i++)
            {
                int switchRow = 0;
                int switchColumn = 0;
                rand = rand%2;
                switch (rand)
                {
                    case 0:
                        switchColumn = EmptyTilePosition.Value;
                        rand2 = rand2%2;
                        switch (rand2)
                        {
                            case 0:
                                switchRow = EmptyTilePosition.Key - 1 >= 0
                                    ? EmptyTilePosition.Key - 1
                                    : EmptyTilePosition.Key + 1;
                                break;
                            case 1:
                                switchRow = EmptyTilePosition.Key + 1 < PuzzleBoard.ColumnSize
                                    ? EmptyTilePosition.Key + 1
                                    : EmptyTilePosition.Key - 1;
                                break;
                        }
                        break;
                    case 1:
                        switchRow = EmptyTilePosition.Key;
                        rand2 = rand2%2;
                        switch (rand2)
                        {
                            case 0:
                                switchColumn = EmptyTilePosition.Value - 1 >= 0
                                    ? EmptyTilePosition.Value - 1
                                    : EmptyTilePosition.Value + 1;
                                break;
                            case 1:
                                switchColumn = EmptyTilePosition.Value + 1 < PuzzleBoard.RowSize
                                    ? EmptyTilePosition.Value + 1
                                    : EmptyTilePosition.Value - 1;
                                break;
                        }
                        break;
                }
                PuzzleSolution.Moves.Add(new KeyValuePair<int, int>(switchRow,switchColumn));
                Swap(switchRow, switchColumn);
                rand++;
                rand2++;
            }
            PuzzleSolution.InitialPositions = PuzzleBoard.Positions;
        }

        public void Swap(int row, int column)
        {
            int emptyRow = EmptyTilePosition.Key;
            int emptyColumn = EmptyTilePosition.Value;

            int emptyPosition = 0, swapPosition = 0;

            for (int i = 0; i < PuzzleBoard.Positions.Count; i++)
            {
                if (PuzzleBoard.Positions[i].X == emptyRow && PuzzleBoard.Positions[i].Y == emptyColumn)
                {
                    emptyPosition = i;
                }
                if (PuzzleBoard.Positions[i].X == row && PuzzleBoard.Positions[i].Y == column)
                {
                    swapPosition = i;
                }
            }

            PuzzleBoard.Positions[emptyPosition].X = row;
            PuzzleBoard.Positions[emptyPosition].Y = column;

            PuzzleBoard.Positions[swapPosition].X = emptyRow;
            PuzzleBoard.Positions[swapPosition].Y = emptyColumn;

            EmptyTilePosition = new KeyValuePair<int, int>(row, column);

        }

        public bool CheckMoveValidity(int row, int column)
        {
            bool isValid = false;

            if (!(row == EmptyTilePosition.Key && column == EmptyTilePosition.Value))
            {

                if (row == EmptyTilePosition.Key &&
                    (column + 1 == EmptyTilePosition.Value || column - 1 == EmptyTilePosition.Value))
                {
                    isValid = true;
                }
                if (column == EmptyTilePosition.Value &&
                    (row + 1 == EmptyTilePosition.Key || row - 1 == EmptyTilePosition.Key))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        #endregion
    }
}
