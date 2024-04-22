using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;


namespace Freelance_Platform_Final.Client
{
    public sealed partial class ClientProfile : Page
    {
        public ClientProfile()
        {
            InitializeComponent();
        }

        readonly DBConnection database = new();

        private bool Freelancer_Profile()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new("INSERT INTO ClientProfile (Firstname, Lastname, Email, Username, Phone, Country, District, PreviousFreelancer, Company, Interests) VALUES (@Firstname, @Lastname, @Email, @Username, @Phone, @Country, @District, @PreviousFreelancer, @Company, @Interests)", database.GetConnection());
            newFreelancer.Parameters.Add("@Firstname", MySqlDbType.VarChar).Value = FirstnameTextBox.Text;
            newFreelancer.Parameters.Add("@Lastname", MySqlDbType.VarChar).Value = LastnameTextBox.Text;
            newFreelancer.Parameters.Add("@Email", MySqlDbType.VarChar).Value = EmailTextBox.Text;
            newFreelancer.Parameters.Add("@Username", MySqlDbType.VarChar).Value = UsernameTextBox.Text;
            newFreelancer.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = PhoneTextBox.Text;
            newFreelancer.Parameters.Add("@Country", MySqlDbType.VarChar).Value = CountryTextBox.Text;
            newFreelancer.Parameters.Add("@District", MySqlDbType.VarChar).Value = DistrictTextBox.Text;
            newFreelancer.Parameters.Add("@PreviousFreelancer", MySqlDbType.VarChar).Value = PrevoiousFreelancerTextBox.Text;
            newFreelancer.Parameters.Add("@Company", MySqlDbType.VarChar).Value = CompanyTextBox.Text;
            newFreelancer.Parameters.Add("@Interests", MySqlDbType.VarChar).Value = InterestsTextBox.Text;

            if (newFreelancer.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClientDashboard()
        {
            Frame.Navigate(typeof(ClientDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void Clientprofilebutton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstnameTextBox.Text) || string.IsNullOrEmpty(LastnameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || string.IsNullOrEmpty(CountryTextBox.Text) || string.IsNullOrEmpty(DistrictTextBox.Text) || string.IsNullOrEmpty(CompanyTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = clientprofilebutton.XamlRoot
                };
                _ = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    if (Freelancer_Profile() == true)
                    {
                        ContentDialog dialog = new()
                        {
                            Title = "Success Message",
                            Content = "Submitted Successfully!",
                            CloseButtonText = "Ok",
                            XamlRoot = clientprofilebutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();
                        ClientDashboard();
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
                        XamlRoot = clientprofilebutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                }
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
