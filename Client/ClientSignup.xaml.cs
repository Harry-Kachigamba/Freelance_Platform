using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;

namespace Freelance_Platform_Final
{
    public sealed partial class ClientSignup : Page
    {
        public ClientSignup()
        {
            this.InitializeComponent();
        }

        //Database Connection Instance
        readonly DBConnection database = new();

        private void HyperlinkButtonClientSignup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        public void Loginfrom()
        {
            Frame.Navigate(typeof(ClientLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private bool Register()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new MySqlCommand("INSERT INTO Client (Firstname, Lastname, Email, Phone, Country, District, Username, Password) VALUES (@FirstnameTextBox, @LastnameTextBox, @EmailTextBox, @PhoneTextBox, @CountryTextBox, @DistrictTextBox, @UsernameTextBox, @PasswordTextBox)", database.GetConnection());
            newFreelancer.Parameters.Add("@FirstnameTextBox", MySqlDbType.VarChar).Value = FirstnameTextBox.Text;
            newFreelancer.Parameters.Add("@LastnameTextBox", MySqlDbType.VarChar).Value = LastnameTextBox.Text;
            newFreelancer.Parameters.Add("@EmailTextBox", MySqlDbType.VarChar).Value = EmailTextBox.Text;
            newFreelancer.Parameters.Add("@PhoneTextBox", MySqlDbType.VarChar).Value = PhoneTextBox.Text;
            newFreelancer.Parameters.Add("@CountryTextBox", MySqlDbType.VarChar).Value = CountryTextBox.Text;
            newFreelancer.Parameters.Add("@DistrictTextBox", MySqlDbType.VarChar).Value = DistrictTextBox.Text;
            newFreelancer.Parameters.Add("@UsernameTextBox", MySqlDbType.VarChar).Value = UsernameTextBox.Text;
            newFreelancer.Parameters.Add("@PasswordTextBox", MySqlDbType.VarChar).Value = PasswordTextBox.Password;

            if (newFreelancer.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void ClientSignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstnameTextBox.Text) || string.IsNullOrEmpty(LastnameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || string.IsNullOrEmpty(CountryTextBox.Text) || string.IsNullOrEmpty(DistrictTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = clientsignupbutton.XamlRoot
                };
                _ = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    if (Register() == true)
                    {
                        ContentDialog dialog = new()
                        {
                            Title = "Success Message",
                            Content = "Sign Up Successful!",
                            CloseButtonText = "Ok",
                            XamlRoot = clientsignupbutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();
                        Loginfrom();
                    }
                }
                catch (MySqlException ex)
                {
                    database.OpenConnection();
                    ContentDialog dialog = new()
                    {
                        Title = "Error Message",
                        Content = ex.Message,
                        CloseButtonText = "Close",
                        XamlRoot = clientsignupbutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                }
            }
        }
    }
}
