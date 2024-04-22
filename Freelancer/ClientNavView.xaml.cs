using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Freelance_Platform_Final.Controller;
using System.Threading.Tasks;
using Freelance_Platform_Final.Models;

namespace Freelance_Platform_Final
{
    public sealed partial class ClientNavView : Page
    {
        public ProfileViewModel ViewModel { get; } = new ProfileViewModel();

        public ClientNavView()
        {
            InitializeComponent();
            Loaded += ProfilesPage_Loaded;
        }

        private async void ProfilesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadProfilesAsync();
        }


        public class ProfileViewModel
        {
            private readonly ProfileService _profileService = new();
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