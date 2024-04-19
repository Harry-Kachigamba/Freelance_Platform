using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Freelance_Platform_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class PDFItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] PDFData { get; set; }
    }

    public sealed partial class AvailableProjectsNavView : Page
    {
        public ObservableCollection<PDFItem> PDFItems { get; set; } = new ObservableCollection<PDFItem>();

        public AvailableProjectsNavView()
        {
            this.InitializeComponent();
            LoadPDFsFromDatabase();
        }

        private void LoadPDFsFromDatabase()
        {
            string connectionString = "server=localhost; port=3307; user id=root; password=12345; database=Freelance";
            string query = "SELECT PdfName, PdfData FROM PdfFiles";

            using (MySqlConnection connection = new(connectionString))
            {
                connection.Open();
                MySqlCommand command = new(query, connection);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString("PdfName");
                    byte[] data = (byte[])reader["PdfData"];

                    PDFItems.Add(new PDFItem
                    {
                        Name = name,
                        PDFData = data
                    }
                    );
                }
            }

            PDFListView.ItemsSource = PDFItems;
        }

        public static async void OpenPdf(PDFItem pdfDocument)
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
                PDFItem selectedPDF = (PDFItem)PDFListView.SelectedItem;

                // Open the selected PDF document
                OpenPdf(selectedPDF);
            }
        }
    }
}
