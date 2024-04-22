using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Freelance_Platform_Final
{
    public sealed partial class LandingPage : Page
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void Landing_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
