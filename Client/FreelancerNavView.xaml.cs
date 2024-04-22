using Freelance_Platform_Final.Controller;
using Freelance_Platform_Final.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Freelance_Platform_Final
{
    public sealed partial class FreelancerNavView : Page
    {
        public ProfileViewModel ViewModel { get; } = new ProfileViewModel();

        public FreelancerNavView()
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
            public ObservableCollection<FreelancerProfile> Profiles { get; } = new ObservableCollection<FreelancerProfile>();

            public async Task LoadProfilesAsync()
            {
                var profiles = await _profileService.GetFreelancerProfilesAsync();
                Profiles.Clear();
                foreach (var item in profiles)
                {
                    Profiles.Add(item);
                }
            }
        }
    }
}
