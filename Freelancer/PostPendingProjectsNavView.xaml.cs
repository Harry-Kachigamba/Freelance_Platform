using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Freelance_Platform_Final.Freelancer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostPendingProjectsNavView : Page
    {
        public PostPendingProjectsNavView()
        {
            this.InitializeComponent();
        }

        private async void UploadPendingPDF_Click(object sender, RoutedEventArgs e)
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
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Insert the file into the database
                    using (var command = new MySqlCommand("INSERT INTO PendingProjects (PdfName, PdfData) VALUES (@projectnametextbox, @pdfData)", connection))
                    {
                        command.Parameters.AddWithValue("@projectnametextbox", MySqlDbType.VarChar).Value = projectnametextbox.Text;
                        command.Parameters.AddWithValue("@pdfData", fileBytes);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            projectnametextbox.Text = "";
        }
    }
}
