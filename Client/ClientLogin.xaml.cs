using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Freelance_Platform_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientLogin : Page
    {
        public ClientLogin()
        {
            this.InitializeComponent();
        }

        DBConnection database = new DBConnection();

        private void HyperlinkButtonClientLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientSignup), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        public void client_dasnboard()
        {
            Frame.Navigate(typeof(ClientDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        //Check if textbox empty
        private async void ClientLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //assigning variables
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            //fetching details from database
            MySqlDataAdapter newFreelanceLogin = new MySqlDataAdapter("SELECT * FROM Client WHERE Username = '" + username + "' and Password = '" + password + "' ", database.GetConnection());
            DataTable freelancerUserTable = new DataTable();
            newFreelanceLogin.Fill(freelancerUserTable);

            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                // Show an error message
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close"
                };

                dialog.XamlRoot = clientloginbutton.XamlRoot;

                ContentDialogResult result = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    if (freelancerUserTable.Rows.Count > 0)
                    {
                        //message box
                        ContentDialog dialog = new ContentDialog
                        {
                            Title = "Success Message",
                            Content = "Successfully Logged In!",
                            CloseButtonText = "Ok"
                        };

                        dialog.XamlRoot = clientloginbutton.XamlRoot;

                        ContentDialogResult result = await dialog.ShowAsync();

                        //page redirection
                        client_dasnboard();
                    }                    
                }

                catch (MySqlException ex)
                {
                    database.OpenConnection();
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Error Message",
                        Content = ex.Message,
                        CloseButtonText = "Close"
                    };

                    dialog.XamlRoot = clientloginbutton.XamlRoot;

                    ContentDialogResult result = await dialog.ShowAsync();
                }
            }
        }
    }

    
}
