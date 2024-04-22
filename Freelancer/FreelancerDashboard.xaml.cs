using Freelance_Platform_Final.Freelancer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace Freelance_Platform_Final
{
    public sealed partial class FreelancerDashboard : Page
    {
        public FreelancerDashboard()
        {
            InitializeComponent();
        }
                

        private void Freelancer_Dasboard_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new()
            {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo
            };
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }

            Type pageType = typeof(ClientNavView);

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == NavItem_Available_Projects.Name)
            {
                pageType = typeof(AvailableProjectsNavView);
            }
            else if (selectedItem.Name == NavItem_Bid_Projects.Name)
            {
                pageType = typeof(BidProjectNavView);
            }
            else if (selectedItem.Name == NavItem_NotCompleted_Projects.Name)
            {
                pageType = typeof(PostPendingProjectsNavView);
            }
            else if (selectedItem.Name == NavItem_Pending_Projects.Name)
            {
                pageType = typeof(PendingProjectsNavView);
            }
            else if (selectedItem.Name == NavItem_PostCompleted_Projects.Name)
            {
                pageType = typeof(PostCompletedProectNavView);
            }
            else if (selectedItem.Name == NavItem_Completed_Projects.Name) {
                pageType= typeof(CompletedProjectsNavView);
            }

            _ = freelancer_nav.Navigate(pageType);
        }

        private void FreelancerAddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FreelancerProfile), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        public void Mainpage()
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void FreelancerLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "Signing Out",
                Content = "Are you sure you want to logout?",
                CloseButtonText = "Yes",
                XamlRoot = FreelancerLogoutButton.XamlRoot
            };
            _ = await dialog.ShowAsync();
            Mainpage();
        }
    }
}
