using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySqlConnector;
using System;

namespace Freelance_Platform_Final.Freelancer
{
    public sealed partial class BidProjectNavView : Page
    {
        public BidProjectNavView()
        {
            this.InitializeComponent();
        }

        readonly DBConnection database = new();


        private bool Freelancer_Profile()
        {
            database.OpenConnection();
            MySqlCommand newFreelancer = new("INSERT INTO BidProjects (Project, Price, Duration) VALUES (@Project, @Price, @Duration)", database.GetConnection());
            newFreelancer.Parameters.Add("@Project", MySqlDbType.VarChar).Value = ProjectTextBox.Text;
            newFreelancer.Parameters.Add("@Price", MySqlDbType.VarChar).Value = PriceTextBox.Text;
            newFreelancer.Parameters.Add("@Duration", MySqlDbType.VarChar).Value = DurationTextBox.Text;
            
            if (newFreelancer.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void BidProjectbutton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text) || string.IsNullOrEmpty(DurationTextBox.Text))
            {
                // Show an error message
                ContentDialog dialog = new()
                {
                    Title = "Error Message",
                    Content = "Please fill in all fields!",
                    CloseButtonText = "Close",
                    XamlRoot = bidprojectbutton.XamlRoot
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
                            XamlRoot = bidprojectbutton.XamlRoot
                        };

                        _ = await dialog.ShowAsync();
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
                        XamlRoot = bidprojectbutton.XamlRoot
                    };
                    _ = await dialog.ShowAsync();
                }
            }
            ProjectTextBox.Text = "";
            PriceTextBox.Text = "";
            DurationTextBox.Text = "";
        }
    }
}
