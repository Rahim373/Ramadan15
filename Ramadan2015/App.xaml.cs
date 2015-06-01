using Ramadan2015.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Ramadan2015
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
			LoadTile();
			LoadLocationAndLocalizing();
			HardwareButtons.BackPressed += HardwareButtons_BackPressed;

			
        }

		private void LoadLocationAndLocalizing()
		{
			Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;

			if (localSetting.Values["Language"].ToString() == "bn-Bd")
			{
				var culture = new CultureInfo("bn-Bd");
				Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
				CultureInfo.DefaultThreadCurrentCulture = culture;
				CultureInfo.DefaultThreadCurrentUICulture = culture;
			}
			else
			{
				var culture = new CultureInfo("en-Us");
				Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
				CultureInfo.DefaultThreadCurrentCulture = culture;
				CultureInfo.DefaultThreadCurrentUICulture = culture;
			}

			if (localSetting.Values["LocationID"].ToString() == "")
			{
				localSetting.Values["Name"] = "Dhaka";
				localSetting.Values["LocationID"] = "14";
				localSetting.Values["Minute"] = "0";
			}
		}

		void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
		{
			Frame frame = Window.Current.Content as Frame;

			if (frame == null)
			{
				return;
			}
			if (frame.CanGoBack)
			{
				frame.GoBack();
				e.Handled = true;
			}
		}

		private void LoadTile()
		{
			XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage05);

			XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
			tileTextAttributes[0].InnerText = "Ifter 6.35 PM";
			tileTextAttributes[1].InnerText = "Sahri 4.08 AM";

			XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
			((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/WideLogo.scale-240.png");
			((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "red graphic");
			((XmlElement)tileImageAttributes[1]).SetAttribute("src", "ms-appx:///Assets/WideLogo.scale-240.png");
			((XmlElement)tileImageAttributes[1]).SetAttribute("alt", "red graphic");


			TileNotification tileNotification = new TileNotification(tileXml);
			tileNotification.ExpirationTime = null;
			TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
		}

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
				//Added by Rahim
				SuspensionManager.RegisterFrame(rootFrame, "rootFrameKey");

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
					await SuspensionManager.RestoreAsync();
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(SplashPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

			await SuspensionManager.SaveAsync();
            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}