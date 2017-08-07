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
using SliddingPuzzle.ADO;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SliddingPuzzle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            AzureTableHelper.CreateTable();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            player.Username = txtUsername.Text;
            player.Password = passwordBox.Password;

            var res = AzureTableHelper.Validate(player).Result;
            if (res)
            {
                App.CurrentUser = player;

                this.Frame.Navigate(typeof (MainPage));
            }
            else
            {
                var dialog = new MessageDialog("Oops.......Not a valid username & passworrd ! ! !");
                await dialog.ShowAsync();
            }
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            player.Username = txtUsername.Text;
            player.Password = passwordBox.Password;

            Task<bool> res = AzureTableHelper.Insert(player);
            if (res.Result)
            {
                App.CurrentUser = player;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var dialog = new MessageDialog("Oops.......Username already taken. Try different name ! ! !");
                await dialog.ShowAsync();
            }
        }
    }
}
