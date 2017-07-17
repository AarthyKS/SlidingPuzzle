using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.PlayTo;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SliddingPuzzle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Button[] buttons = new Button[16];
        private Button empty; // references one of the buttons in `buttons`
        private TextBlock clicksLabel;
        private TextBox moveCountText;
        private int moveCount = 0;
        private Random random = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            InitialSetup();
        }

        private void InitialSetup()
        {
            int current = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    buttons[current] = new Button();
                    if (i == 3 && j == 3)
                    {
                        buttons[current].Content = "";
                    }
                    else
                    {
                        buttons[current].Content = current + 1;
                    }
                    buttons[current].Name = "btn" + current.ToString();
                    buttons[current].Width = 100;
                    buttons[current].Height = 100;
                    Grid.SetRow(buttons[current],i);
                    Grid.SetColumn(buttons[current], j);
                    puzzleGrid.Children.Add(buttons[current]);
                    current++;
                }
            }
        }
        
    }
}
