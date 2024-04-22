using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;

namespace Freelance_Platform_Final
{
    public sealed partial class FreelancerSignup : Page
    {
        public FreelancerSignup()
        {
            InitializeComponent();
        }

        readonly DBConnection database = new();

        private bool Register()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new("INSERT INTO Freelancer (Firstname, Lastname, Email, Phone, Country, District, Profession, Username, Password) VALUES (@Firstname, @Lastname, @Email, @Phone, @Country, @District, @Profession, @Username, @Password)", database.GetConnection());
            newFreelancer.Parameters.Add("@Firstname", MySqlDbType.VarChar).Value = FirstnameTextBox.Text;
            newFreelancer.Parameters.Add("@Lastname", MySqlDbType.VarChar).Value = LastnameTextBox.Text;
            newFreelancer.Parameters.Add("@Email", MySqlDbType.VarChar).Value = EmailTextBox.Text;
            newFreelancer.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = PhoneTextBox.Text;
            newFreelancer.Parameters.Add("@Country", MySqlDbType.VarChar).Value = CountryTextBox.Text;
            newFreelancer.Parameters.Add("@District", MySqlDbType.VarChar).Value = DistrictTextBox.Text;
            newFreelancer.Parameters.Add("@Profession", MySqlDbType.VarChar).Value= ProfessionTextBox.Text;
            newFreelancer.Parameters.Add("@Username", MySqlDbType.VarChar).Value = UsernameTextBox.Text;
            newFreelancer.Parameters.Add("@Password", MySqlDbType.VarChar).Value = PasswordTextBox.Password;

            if (newFreelancer.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Redirect to Login Page
        private void HyperlinkButtonFreelancerSignup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FreelancerLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        public void Loginfrom()
        {
            Frame.Navigate(typeof(FreelancerLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void FreelancerSignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstnameTextBox.Text) || string.IsNullOrEmpty(LastnameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || string.IsNullOrEmpty(CountryTextBox.Text) || string.IsNullOrEmpty(DistrictTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = freelancersignupbutton.XamlRoot
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
                            XamlRoot = freelancersignupbutton.XamlRoot
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
                        XamlRoot = freelancersignupbutton.XamlRoot
                    };

                    _ = await dialog.ShowAsync();
                }
            }
        }
    }
}
