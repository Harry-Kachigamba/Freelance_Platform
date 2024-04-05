using Freelance_Platform_Final.Client;
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
    public sealed partial class ClientDashboard : Page
    {
        public ClientDashboard()
        {
            this.InitializeComponent();
        }

        private void Client_Dasboard_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
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
            ContentDialog dialog = new ContentDialog
            {
                Title = "Signing Out",
                Content = "Are you sure you want to logout?",
                CloseButtonText = "Yes",
            };

            dialog.XamlRoot = ClientLogoutButton.XamlRoot;

            ContentDialogResult result = await dialog.ShowAsync();
            Mainpage();
        }
    }
}
