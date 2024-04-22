using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MySqlConnector;
using System;

namespace Freelance_Platform_Final.Freelancer
{
    public sealed partial class FreelancerProfile : Page
    {
        public FreelancerProfile()
        {
            InitializeComponent();
        }

        readonly DBConnection database = new();


        private bool Freelancer_Profile()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new("INSERT INTO FreelancerProfile (Firstname, Lastname, Email, Phone, Username, Country, District, Skills, Expertise, Pastwork) VALUES (@Firstname, @Lastname, @Email, @Phone, @Username, @Country, @District, @Skills, @Expertise, @PastWork)", database.GetConnection());
            newFreelancer.Parameters.Add("@Firstname", MySqlDbType.VarChar).Value = FirstnameTextBox.Text;
            newFreelancer.Parameters.Add("@Lastname", MySqlDbType.VarChar).Value = LastnameTextBox.Text;
            newFreelancer.Parameters.Add("@Email", MySqlDbType.VarChar).Value = EmailTextBox.Text;
            newFreelancer.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = PhoneTextBox.Text;
            newFreelancer.Parameters.Add("@Username", MySqlDbType.VarChar).Value = UsernameTextBox.Text;
            newFreelancer.Parameters.Add("@Country", MySqlDbType.VarChar).Value = CountryTextBox.Text;
            newFreelancer.Parameters.Add("@District", MySqlDbType.VarChar).Value = DistrictTextBox.Text;
            newFreelancer.Parameters.Add("@Skills", MySqlDbType.VarChar).Value = SkillsTextBox.Text;
            newFreelancer.Parameters.Add("@Expertise", MySqlDbType.VarChar).Value = ExpertiseTextBox.Text;
            newFreelancer.Parameters.Add("@PastWork", MySqlDbType.VarChar).Value = PastWorkTextBox.Text;

            if (newFreelancer.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FreelancerDashboard()
        {
            Frame.Navigate(typeof(FreelancerDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void Freelancerprofilebutton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstnameTextBox.Text) || string.IsNullOrEmpty(LastnameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || string.IsNullOrEmpty(CountryTextBox.Text) || string.IsNullOrEmpty(DistrictTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = freelancerprofilebutton.XamlRoot
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
                            XamlRoot = freelancerprofilebutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();
                        FreelancerDashboard();
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
                        XamlRoot = freelancerprofilebutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                }
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FreelancerDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
