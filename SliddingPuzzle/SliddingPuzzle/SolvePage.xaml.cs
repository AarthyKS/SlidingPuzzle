using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SliddingPuzzle.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SliddingPuzzle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SolvePage : Page
    {
        private Button[] buttons = new Button[16];
        private Puzzle myPuzzle;
        public SolvePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Puzzle) e.Parameter;
            myPuzzle = parameters;
            LoadSetup();
        }

        private async void LoadSetup()
        {
            foreach (Tile tile in myPuzzle.PuzzleBoard.Positions)
            {
                int current = Convert.ToInt32(tile.Id) - 1;
                buttons[current] = new Button();
                buttons[current].Name = "btn" + tile.Id;
                buttons[current].Width = 100;
                buttons[current].Height = 100;
                buttons[current].Content = tile.Value;
                Grid.SetRow(buttons[current], tile.X);
                Grid.SetColumn(buttons[current], tile.Y);
                puzzleGrid.Children.Add(buttons[current]);
                if (current == 15)
                {
                    myPuzzle.EmptyTilePosition = new KeyValuePair<int, int>(tile.X, tile.Y);
                }
            }

            if (!myPuzzle.PuzzleSolution.isAI)
            {
                for (int i = myPuzzle.PuzzleSolution.Moves.Count - 1; i >= 0; i--)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    Move(myPuzzle.PuzzleSolution.Moves[i].Key, myPuzzle.PuzzleSolution.Moves[i].Value);
                }
                Move(3, 3);
            }
            else
            {
                for (int i = 0; i <= myPuzzle.PuzzleSolution.Moves.Count - 1; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    Move(myPuzzle.PuzzleSolution.Moves[i].Key, myPuzzle.PuzzleSolution.Moves[i].Value);
                }
                Move(3, 3);
            }
        }
        private async void Move(int x, int y)
        {
            Button clicked = null;

            int columnSender = y;
            int rowSender = x;

            int emptyPosition = 0;

            for (int i = 0; i < buttons.Length; i++)
            {
                int columnA = Grid.GetColumn(buttons[i]);
                int rowA = Grid.GetRow(buttons[i]);
                if (rowA == x & columnA == y)
                {
                    clicked = buttons[i];
                    break;
                }
            }

            if (myPuzzle.CheckMoveValidity(rowSender, columnSender))
            {
                myPuzzle.Swap(rowSender, columnSender);
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].Name == "btn16")
                    {
                        emptyPosition = i;
                    }
                }
                swapLocations(clicked, buttons[emptyPosition]);
            }
        }

        private void swapLocations(Button a, Button b)
        {
            int columnA = Grid.GetColumn(a);
            int rowA = Grid.GetRow(a);

            int columnB = Grid.GetColumn(b);
            int rowB = Grid.GetRow(b);

            Grid.SetColumn(a, columnB);
            Grid.SetRow(a, rowB);

            Grid.SetColumn(b, columnA);
            Grid.SetRow(b, rowA);
        }
    }
}
