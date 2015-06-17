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

    public sealed partial class App : Application
    {
        private TransitionCollection transitions;
		Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;

     
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
			HardwareButtons.BackPressed += HardwareButtons_BackPressed;
			this.UnhandledException += App_UnhandledException;
			setSetting();
			LiveTile();
        }

		void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			showMsg(e.ToString());
		}

		async void showMsg(string e)
		{
			MessageDialog msg = new MessageDialog(e.ToString(), "Error");
			await msg.ShowAsync();
		}

		private void LiveTile()
		{
			XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage05);

			XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
			tileTextAttributes[0].InnerText = "সেহরি ইফতার এর সময়";
			tileTextAttributes[1].InnerText = "শুধুমাত্র বাংলাদেশের জন্যে";

			XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
			((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/WideLogo.scale-240.png");
			((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "red graphic");
			((XmlElement)tileImageAttributes[1]).SetAttribute("src", "ms-appx:///Assets/WideLogo.scale-240.png");
			((XmlElement)tileImageAttributes[1]).SetAttribute("alt", "red graphic");

			TileNotification tileNotification = new TileNotification(tileXml);
			tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddMonths(1);
			TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
		}

		private void setSetting() {
			localSetting.Values["Language"] = "bn-BD";
			var culture = new CultureInfo("bn-BD");
			Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
			CultureInfo.DefaultThreadCurrentCulture = culture;

			if (localSetting.Values["LocationID"] == null)
			{
				localSetting.Values["Name"] = "ঢাকা";
				localSetting.Values["LocationID"] = "14";
				localSetting.Values["Minute"] = "0";
				localSetting.Values["Language"] = "bn-BD";

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

        protected override void OnLaunched(LaunchActivatedEventArgs e)
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
			

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
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

        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}