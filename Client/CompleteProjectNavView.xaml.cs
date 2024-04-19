using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.System;

namespace Freelance_Platform_Final
{
    public class CompletedProjectClientPDFItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] PDFData { get; set; }
    }

    public sealed partial class CompleteProjectNavView : Page
    {
        public ObservableCollection<CompletedProjectClientPDFItem> PDFItems { get; set; } = new ObservableCollection<CompletedProjectClientPDFItem>();

        public CompleteProjectNavView()
        {
            this.InitializeComponent();
            LoadCompletedProjectsClientFromDatabase();
        }

        private void LoadCompletedProjectsClientFromDatabase()
        {
            string connectionString = "server=localhost; port=3307; user id=root; password=12345; database=Freelance";
            string query = "SELECT PdfName, PdfData FROM CompletedProjects";

            using (MySqlConnection connection = new(connectionString))
            {
                connection.Open();
                MySqlCommand command = new(query, connection);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString("PdfName");
                    byte[] data = (byte[])reader["PdfData"];

                    PDFItems.Add(new CompletedProjectClientPDFItem
                    {
                        Name = name,
                        PDFData = data
                    }
                    );
                }
            }

            PDFListView.ItemsSource = PDFItems;
        }

        public static async void OpenPdf(CompletedProjectClientPDFItem pdfDocument)
        {
            try
            {
                // Get the local folder for temporary files
                StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;

                // Create a new temporary PDF file
                StorageFile pdfFile = await tempFolder.CreateFileAsync($"{pdfDocument.Name}.pdf", CreationCollisionOption.ReplaceExisting);

                // Write the byte array to the new file
                await FileIO.WriteBytesAsync(pdfFile, pdfDocument.PDFData);

                // Launch the PDF file with the default system viewer
                if (pdfFile != null)
                {
                    await Launcher.LaunchFileAsync(pdfFile);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }

        }

        private void PDFListView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (PDFListView.SelectedItem != null)
            {
                // Get the selected PDF item from the PDFListView
                CompletedProjectClientPDFItem selectedPDF = (CompletedProjectClientPDFItem)PDFListView.SelectedItem;

                // Open the selected PDF document
                OpenPdf(selectedPDF);
            }
        }
    }
}

