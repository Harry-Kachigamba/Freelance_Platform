using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySqlConnector;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;

namespace Freelance_Platform_Final
{
    public sealed partial class PostProjectNavView : Page
    {
        public PostProjectNavView()
        {
            InitializeComponent();
        }

        private async void UploadPDF_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            var window = App.MainWindow;
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.FileTypeFilter.Add(".pdf");
     
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                byte[] fileBytes;

                // Read file into byte array
                using (var stream = await file.OpenReadAsync())
                {
                    fileBytes = new byte[stream.Size];
                    await stream.ReadAsync(fileBytes.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);
                    PickedPDFfileTextBlock.Text = "Picked PDF: " + file.Name;
                }

                // Connect to MySQL database
                string connectionString = "server=localhost; port=3307; user id=root; password=12345; database=Freelance";
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                // Insert the file into the database
                using var command = new MySqlCommand("INSERT INTO PostProjects (PdfName, PdfData) VALUES (@projectnametextbox, @pdfData)", connection);
                command.Parameters.AddWithValue("@projectnametextbox", MySqlDbType.VarChar).Value = projectnametextbox.Text;
                command.Parameters.AddWithValue("@pdfData", fileBytes);
                await command.ExecuteNonQueryAsync();
            }

            projectnametextbox.Text = "";
        }
    }
}