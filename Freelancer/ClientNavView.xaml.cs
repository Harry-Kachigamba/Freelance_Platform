using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Contacts;
using Windows.Networking.NetworkOperators;
using Freelance_Platform_Final;
using Freelance_Platform_Final.Controller;
using System.Threading.Tasks;
using Freelance_Platform_Final.Models;


namespace Freelance_Platform_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientNavView : Page
    {
        public ProfileViewModel ViewModel { get; } = new ProfileViewModel();

        public ClientNavView()
        {
            this.InitializeComponent();
            this.Loaded += ProfilesPage_Loaded;
        }

        private async void ProfilesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadProfilesAsync();
        }


        public class ProfileViewModel
        {
            private ProfileService _profileService = new ProfileService();
            public ObservableCollection<ClientProfile> Profiles { get; } = new ObservableCollection<ClientProfile>();

            public async Task LoadProfilesAsync()
            {
                var profiles = await _profileService.GetClientProfilesAsync();
                Profiles.Clear();
                foreach (var profile in profiles)
                {
                    Profiles.Add(profile);
                }
            }
        }
    }
}