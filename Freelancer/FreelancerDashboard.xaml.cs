using Freelance_Platform_Final.Freelancer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Freelance_Platform_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreelancerDashboard : Page
    {
        public FreelancerDashboard()
        {
            this.InitializeComponent();
        }
                

        private void Freelancer_Dasboard_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
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
            else if (selectedItem.Name == NavItem_Pending_Projects.Name)
            {
                pageType = typeof(PendingProjectsNavView);
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
            ContentDialog dialog = new ContentDialog
            {
                Title = "Signing Out",
                Content = "Are you sure you want to logout?",
                CloseButtonText = "Yes",
            };

            dialog.XamlRoot = FreelancerLogoutButton.XamlRoot;

            ContentDialogResult result = await dialog.ShowAsync();
            Mainpage();
        }
    }
}
