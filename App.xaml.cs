using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace Freelance_Platform_Final
{
    public partial class App : Application
    {
        public static MainWindow MainWindow = new();

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            //create a frame to act as the navigation context and navigate to the first page
            Frame rootFrame = new();
            rootFrame.NavigationFailed += OnNavigationFailed;
            //navigate to the first page, configuring the new page
            //by passing required infromation as a navigation parameter
            rootFrame.Navigate(typeof(LandingPage), args.Arguments);

            //place the frame in the current window
            m_window.Content = rootFrame;
            //ensure the mainwindows is active
            m_window.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        public Window m_window;

        //public static object MainWindow { get; internal set; }
        //public static object LandingPage { get; set; }
    }
}
