using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using MySqlConnector;
using Windows.Storage;
using Windows.System;

namespace Freelance_Platform_Final
{
    public class PendingProjectPDFItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] PDFData { get; set; }
    }

    public sealed partial class PendingProjectsNavView : Page
    {
        public ObservableCollection<PendingProjectPDFItem> PDFItems { get; set; } = new ObservableCollection<PendingProjectPDFItem>();

        public PendingProjectsNavView()
        {
            InitializeComponent();
            LoadPendingProjectsFromDatabase();
        }

            private void LoadPendingProjectsFromDatabase()
            {
                string connectionString = "xxxxxxxxxxxxxx";
                string query = "SELECT PdfName, PdfData FROM PendingProjects";

                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new(query, connection);

                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = reader.GetString("PdfName");
                        byte[] data = (byte[])reader["PdfData"];

                        PDFItems.Add(new PendingProjectPDFItem
                        {
                            Name = name,
                            PDFData = data
                        }
                        );
                    }
                }

            PDFListView.ItemsSource = PDFItems;
        }

        public static async void OpenPdf(PendingProjectPDFItem pdfDocument)
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
                PendingProjectPDFItem selectedPDF = (PendingProjectPDFItem)PDFListView.SelectedItem;

                // Open the selected PDF document
                OpenPdf(selectedPDF);
            }
        }
    }

}
