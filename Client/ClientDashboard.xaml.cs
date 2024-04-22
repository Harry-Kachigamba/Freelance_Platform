using Freelance_Platform_Final.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace Freelance_Platform_Final
{
    public sealed partial class ClientDashboard : Page
    {
        public ClientDashboard()
        {
            InitializeComponent();
        }

        private void Client_Dasboard_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new()
            {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo
            };
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }

            Type pageType = typeof(FreelancerNavView);

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == NavItem_Post_Project.Name)
            {
                pageType = typeof(PostProjectNavView);
            }
            else if (selectedItem.Name == NavItem_Bid_Project.Name)
            {
                pageType = typeof(ProjectBid);
            }
            else if (selectedItem.Name == NavItem_Completed_Project.Name)
            {
                pageType = typeof(CompleteProjectNavView);
            }
            
            _ = client_nav.Navigate(pageType);
        }

        private void ClientAddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientProfile), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        public void Mainpage()
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void ClientLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "Signing Out",
                Content = "Are you sure you want to logout?",
                CloseButtonText = "Yes",
                XamlRoot = ClientLogoutButton.XamlRoot
            };
            _ = await dialog.ShowAsync();
            Mainpage();
        }
    }
}
