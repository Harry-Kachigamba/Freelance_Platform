using Freelance_Platform_Final.Controller;
using Freelance_Platform_Final.Models.Bid_System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Freelance_Platform_Final.Client
{
    public sealed partial class ProjectBid : Page
    {
        public ProjectBidViewModel ViewModel { get; } = new ProjectBidViewModel();

        public ProjectBid()
        {
            this.InitializeComponent();
            this.Loaded += ProjectsPage_Loaded;
        }

        private async void ProjectsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadProjectAsync();
        }

        public class ProjectBidViewModel
        {
            private readonly BidService _projectService = new();
            public ObservableCollection<BidProject> Projects { get; } = new ObservableCollection<BidProject>();

            public async Task LoadProjectAsync()
            {
                var projects = await _projectService.GetProjectBidsAsync();
                Projects.Clear();
                foreach (var item in projects)
                {
                    Projects.Add(item);
                }
            }
        }
    }
}
