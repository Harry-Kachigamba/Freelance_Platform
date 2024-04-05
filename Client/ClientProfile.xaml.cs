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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Freelance_Platform_Final.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientProfile : Page
    {
        public ClientProfile()
        {
            this.InitializeComponent();
        }

        DBConnection database = new DBConnection();

        private bool Freelancer_Profile()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new MySqlCommand("INSERT INTO ClientProfile (Firstname, Lastname, Email, Username, Phone, Country, District, PreviousFreelancer, Company, Interests) VALUES (@Firstname, @Lastname, @Email, @Username, @Phone, @Country, @District, @PreviousFreelancer, @Company, @Interests)", database.GetConnection());
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

        public void clientDashboard()
        {
            Frame.Navigate(typeof(ClientDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void clientprofilebutton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstnameTextBox.Text) || string.IsNullOrEmpty(LastnameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || string.IsNullOrEmpty(CountryTextBox.Text) || string.IsNullOrEmpty(DistrictTextBox.Text) || string.IsNullOrEmpty(CompanyTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                // Show an error message
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close"
                };

                dialog.XamlRoot = clientprofilebutton.XamlRoot;

                ContentDialogResult result = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    if (Freelancer_Profile() == true)
                    {
                        ContentDialog dialog = new ContentDialog
                        {
                            Title = "Success Message",
                            Content = "Submitted Successfully!",
                            CloseButtonText = "Ok"
                        };

                        dialog.XamlRoot = clientprofilebutton.XamlRoot;

                        ContentDialogResult result = await dialog.ShowAsync();
                        clientDashboard();
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

                    dialog.XamlRoot = clientprofilebutton.XamlRoot;

                    ContentDialogResult result = await dialog.ShowAsync();
                }
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientDashboard), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
