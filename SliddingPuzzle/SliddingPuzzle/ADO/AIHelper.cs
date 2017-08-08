using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using SliddingPuzzle.Model;

namespace SliddingPuzzle.ADO
{
    public static class AIHelper
    { 
        public static List<KeyValuePair<int, int>> SolveWithAI(List<Tile> initialPosition)
        {
            List<KeyValuePair<int, int>> completed = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    completed.Add(new KeyValuePair<int, int>(i,j));
                }
            }
            KeyValuePair<int, int> emptyPosition;

            List<KeyValuePair<int, int>> moves = new List<KeyValuePair<int, int>>();

            int min;

            do
            {
                List<List<Tile>> options = new List<List<Tile>>();

                for (int i = initialPosition.Count - 1; i >= 0; i--)
                {
                    if (initialPosition[i].Id == initialPosition.Count.ToString())
                    {
                        emptyPosition = new KeyValuePair<int, int>(initialPosition[i].X, initialPosition[i].Y);
                        break;
                    }
                }

                if (emptyPosition.Key + 1 <= 3)
                {
                    options.Add(Swap(emptyPosition.Key+1,emptyPosition.Value,emptyPosition.Key,emptyPosition.Value, initialPosition.Select(tile => new Tile() {Id = tile.Id,Value = tile.Value,X= tile.X,Y= tile.Y}).ToList()));
                }

                if (emptyPosition.Key - 1 >= 0)
                {
                    options.Add(Swap(emptyPosition.Key - 1, emptyPosition.Value, emptyPosition.Key, emptyPosition.Value, initialPosition.Select(tile => new Tile() { Id = tile.Id, Value = tile.Value, X = tile.X, Y = tile.Y }).ToList()));
                }
                if (emptyPosition.Value + 1 <= 3)
                {
                    options.Add(Swap(emptyPosition.Key , emptyPosition.Value+1, emptyPosition.Key, emptyPosition.Value, initialPosition.Select(tile => new Tile() { Id = tile.Id, Value = tile.Value, X = tile.X, Y = tile.Y }).ToList()));
                }

                if (emptyPosition.Value - 1 >= 0)
                {
                    options.Add(Swap(emptyPosition.Key, emptyPosition.Value-1, emptyPosition.Key, emptyPosition.Value, initialPosition.Select(tile => new Tile() { Id = tile.Id, Value = tile.Value, X = tile.X, Y = tile.Y }).ToList()));
                }

                List<int> lstCount = new List<int>();
                foreach (var possibilities in options)
                {
                    int count = 0;
                    int i = 0;
                    foreach (var tile in possibilities)
                    {
                        if ((tile.X != completed[i].Key || tile.Y != completed[i].Value))
                        {
                            count++;
                        }
                        i++;
                    }
                    lstCount.Add(count);
                }

                min = lstCount.Min();

                int minIndex = lstCount.FindIndex(a => a == min);

                switch (minIndex)
                {
                    case 0:
                        moves.Add(new KeyValuePair<int, int>(emptyPosition.Key + 1, emptyPosition.Value));
                        break;
                    case 1:
                        moves.Add(new KeyValuePair<int, int>(emptyPosition.Key - 1, emptyPosition.Value));
                        break;
                    case 2:
                        moves.Add(new KeyValuePair<int, int>(emptyPosition.Key, emptyPosition.Value+1));
                        break;
                    case 3:
                        moves.Add(new KeyValuePair<int, int>(emptyPosition.Key , emptyPosition.Value-1));
                        break;
                }
                initialPosition = options[minIndex];

            } while (min > 0);

            return moves;
        }

        public  static List<Tile> Swap(int row, int column,int emptyRow,int emptyColumn,List<Tile> positions )
        {
            int emptyPosition = 0, swapPosition = 0;

            for (int i = 0; i <positions.Count; i++)
            {
                if (positions[i].X == emptyRow && positions[i].Y == emptyColumn)
                {
                    emptyPosition = i;
                }
                if (positions[i].X == row && positions[i].Y == column)
                {
                    swapPosition = i;
                }
            }

            positions[emptyPosition].X = row;
            positions[emptyPosition].Y = column;

            positions[swapPosition].X = emptyRow;
            positions[swapPosition].Y = emptyColumn;

            return positions;
        }
    }
}
