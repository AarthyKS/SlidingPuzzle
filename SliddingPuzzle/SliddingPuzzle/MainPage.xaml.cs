using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.PlayTo;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using SliddingPuzzle.ADO;
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
        private Puzzle myPuzzle;


        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Puzzle) e.Parameter;

            if (parameters == null)
            {
                this.myPuzzle = new Puzzle(4, 4);
                InitialSetup();
            }
            else
            {
                myPuzzle = parameters;
                LoadSetup();
            }
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

        private void LoadSetup()
        {
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
                if (current == 15)
                {
                    myPuzzle.EmptyTilePosition = new KeyValuePair<int, int>(tile.X, tile.Y);
                }
            }
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            int columnSender = Grid.GetColumn(clicked);
            int rowSender = Grid.GetRow(clicked);

            int emptyPosition = 0;

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

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            Player player = new Player();
            player.Username = App.CurrentUser.Username;
            player.Password = App.CurrentUser.Password;
            game.HashedGame = AzureTableHelper.GetPlayer(player).Result.SavedGameId;
            if (!string.IsNullOrEmpty(game.HashedGame))
            {
                Game resGame = AzureTableHelper.GetGame(game).Result;
                Puzzle saved = new Puzzle(4, 4);
                saved.PuzzleBoard = (Board) JsonConvert.DeserializeObject<Board>(resGame.CurrentGame);
                saved.PuzzleSolution = (Solution) JsonConvert.DeserializeObject<Solution>(resGame.Solution);
                this.Frame.Navigate(typeof (MainPage), saved);
            }
            else
            {
                var dialog = new MessageDialog("Oops.......No saved game ! ! !");
                await dialog.ShowAsync();
            }
        }

        private async void BtnSave_Game_OnClick(object sender, RoutedEventArgs e)
        {
            Game currentGame = new Game();
            currentGame.CurrentGame = JsonConvert.SerializeObject(myPuzzle.PuzzleBoard);
            currentGame.HashedGame = currentGame.CurrentGame.GetHashCode().ToString();
            currentGame.Solution = JsonConvert.SerializeObject(myPuzzle.PuzzleSolution);

            await AzureTableHelper.Insert(currentGame);
            Player player = new Player();
            player.Password = App.CurrentUser.Password;
            player.Username = App.CurrentUser.Username;
            player.SavedGameId = currentGame.HashedGame;

            await AzureTableHelper.Update(player);

            App.CurrentUser.SavedGameId = currentGame.HashedGame;

            var dialog = new MessageDialog("Game saved!!!!!!!");
            await dialog.ShowAsync();
        }

        private async void BtnSolve_OnClick(object sender, RoutedEventArgs e)
        {
            Puzzle passParameter = new Puzzle(myPuzzle.PuzzleBoard.RowSize, myPuzzle.PuzzleBoard.ColumnSize);
            passParameter.PuzzleBoard.Positions = myPuzzle.PuzzleSolution.InitialPositions;
            passParameter.PuzzleSolution = myPuzzle.PuzzleSolution;
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                async () =>
                {
                    var newWindow = Window.Current;
                    var newAppView = ApplicationView.GetForCurrentView();
                    newAppView.Title = "Solve window";

                    var frame = new Frame();
                    frame.Navigate(typeof (SolvePage), passParameter);
                    newWindow.Content = frame;
                    newWindow.Activate();

                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                        newAppView.Id,
                        ViewSizePreference.UseMinimum,
                        currentAV.Id,
                        ViewSizePreference.UseMinimum);
                });
        }

        private async void BtnSolveAI_OnClick(object sender, RoutedEventArgs e)
        {
            List<KeyValuePair<int, int>> solution = AIHelper.SolveWithAI(myPuzzle.PuzzleBoard.Positions);

            Puzzle passParameter = new Puzzle(myPuzzle.PuzzleBoard.RowSize, myPuzzle.PuzzleBoard.ColumnSize);
            passParameter.PuzzleBoard.Positions = myPuzzle.PuzzleBoard.Positions;
            passParameter.PuzzleSolution = new Solution();
            passParameter.PuzzleSolution.Moves = solution;
            passParameter.PuzzleSolution.isAI = true;
            passParameter.PuzzleSolution.InitialPositions = myPuzzle.PuzzleBoard.Positions;
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                async () =>
                {
                    var newWindow = Window.Current;
                    var newAppView = ApplicationView.GetForCurrentView();
                    newAppView.Title = "Solve window";

                    var frame = new Frame();
                    frame.Navigate(typeof (SolvePage), passParameter);
                    newWindow.Content = frame;
                    newWindow.Activate();

                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                        newAppView.Id,
                        ViewSizePreference.UseMinimum,
                        currentAV.Id,
                        ViewSizePreference.UseMinimum);
                });
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            this.Frame.Navigate(typeof(Login));
        }
    }
}
