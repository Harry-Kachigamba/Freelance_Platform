using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;
using System.Data;

namespace Freelance_Platform_Final
{
    public sealed partial class ClientLogin : Page
    {
        public ClientLogin()
        {
            InitializeComponent();
        }

        readonly DBConnection database = new();

        private void HyperlinkButtonClientLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientSignup), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ReturnHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        public void ClientDashboard()
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
            MySqlDataAdapter newFreelanceLogin = new("SELECT * FROM Client WHERE Username = '" + username + "' and Password = '" + password + "' ", database.GetConnection());
            DataTable freelancerUserTable = new();
            newFreelanceLogin.Fill(freelancerUserTable);

            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = clientloginbutton.XamlRoot
                };
                _ = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    if (freelancerUserTable.Rows.Count > 0)
                    {
                        //message box
                        ContentDialog dialog = new()
                        {
                            Title = "Success Message",
                            Content = "Successfully Logged In!",
                            CloseButtonText = "Ok",
                            XamlRoot = clientloginbutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();

                        //page redirection
                        ClientDashboard();
                    }                    
                }

                catch (Exception)
                {
                    ContentDialog dialog = new()
                    {
                        Title = "Error Message",
                        Content = "User does not exist",
                        CloseButtonText = "Ok",
                        XamlRoot = clientloginbutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                    return;
                }
            }
        }
    }

    
}
