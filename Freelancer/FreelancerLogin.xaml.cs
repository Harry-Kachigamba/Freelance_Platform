using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;
using System.Data;

namespace Freelance_Platform_Final
{
    public sealed partial class FreelancerLogin : Page
    {
        public FreelancerLogin()
        {
            this.InitializeComponent();
        }

        readonly DBConnection database = new();

        //Redirects to Signup 
        private void HyperlinkButtonFreelancer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FreelancerSignup), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ReturnHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        //Method, redirects to freelancer dashboard
        public void FreelancerDashboard()
        {
            Frame.Navigate(typeof(FreelancerDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void FreelancerLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //assigning variables
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            //fetching details from database
            MySqlDataAdapter newFreelanceLogin = new("SELECT * FROM Freelancer WHERE Username = '" + username + "' and Password = '" + password + "' ", database.GetConnection());
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
                    XamlRoot = freelancerloginbutton.XamlRoot
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
                            XamlRoot = freelancerloginbutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();

                        //page redirection
                        FreelancerDashboard();
                    }
                }

                catch (Exception)
                {
                    ContentDialog dialog = new()
                    {
                        Title = "Error Message",
                        Content = "User does not exist",
                        CloseButtonText = "Ok",
                        XamlRoot = freelancerloginbutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                    return;
                }
            }
        }

    }
}
