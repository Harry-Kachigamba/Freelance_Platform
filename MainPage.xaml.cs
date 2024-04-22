using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Freelance_Platform_Final
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPageClientButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void MainPageFreelancerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FreelancerLogin), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
