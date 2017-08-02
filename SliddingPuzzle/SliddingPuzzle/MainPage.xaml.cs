using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.PlayTo;
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
    public sealed partial class MainPage : Page
    {
        private Button[] buttons = new Button[16];
        private Puzzle myPuzzle ;


        public MainPage()
        {
            this.myPuzzle = new Puzzle(4,4);
            this.InitializeComponent();
            InitialSetup();
        }

        private void InitialSetup()
        {
            myPuzzle.GenerateNumericBoard();
            myPuzzle.MixPuzzle();
            foreach (Tile tile in myPuzzle.PuzzleBoard.Positions)
            {
               int current = Convert.ToInt32(tile.Id) - 1;
                buttons[current] = new Button();
                buttons[current].Name = "btn" + tile.Id;
                buttons[current].Width = 100;
                buttons[current].Height = 100;
                buttons[current].Content = tile.Value;
                buttons[current].Click += btn_Click;
                Grid.SetRow(buttons[current], tile.X);
                Grid.SetColumn(buttons[current], tile.Y);
                puzzleGrid.Children.Add(buttons[current]);
            }
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            int columnSender = Grid.GetColumn(clicked);
            int rowSender = Grid.GetRow(clicked);

            int emptyPosition = 0;

            if (myPuzzle.CheckMoveValidity(rowSender,columnSender))
            {
                myPuzzle.Swap(rowSender,columnSender);
                for (int i = 0; i < buttons.Length;i++)
                {
                    if (buttons[i].Name == "btn16")
                    {
                        emptyPosition = i;
                    }
                }
                swapLocations(clicked, buttons[emptyPosition]);
            }
            else
            {
                var dialog = new MessageDialog("Oops.......Not a valid move ! ! !");
                await dialog.ShowAsync();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (MainPage));
        }
    }
}
