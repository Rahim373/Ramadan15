using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ramadan2015
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SettingPage : Page
	{
		Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;
		public SettingPage()
		{
			this.InitializeComponent();
			this.Loaded += SettingPage_Loaded;
		}

		void SettingPage_Loaded(object sender, RoutedEventArgs e)
		{
			if (localSetting.Values["Language"].ToString() != "bn-Bd")
			{
				cheackBox.IsOn = false;
			}
			else
			{
				cheackBox.IsOn = true;
			}
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

		}

		//private void cheackBox_Checked(object sender, RoutedEventArgs e)
		//{

		//	localSetting.Values["Language"] = "bn-Bd";

		//	var culture = new CultureInfo("bn-Bd");
		//	Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
		//	CultureInfo.DefaultThreadCurrentCulture = culture;
		//	CultureInfo.DefaultThreadCurrentUICulture = culture;
		//}

		//private void cheackBox_Unchecked(object sender, RoutedEventArgs e)
		//{
		//	localSetting.Values["Language"] = "en-Us";

		//	var culture = new CultureInfo("en-Us");
		//	Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
		//	CultureInfo.DefaultThreadCurrentCulture = culture;
		//	CultureInfo.DefaultThreadCurrentUICulture = culture;
		//}

		private void cheackBox_Toggled(object sender, RoutedEventArgs e)
		{
			if (cheackBox.IsOn)
			{
				localSetting.Values["Language"] = "bn-Bd";

				var culture = new CultureInfo("bn-Bd");
				Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
				CultureInfo.DefaultThreadCurrentCulture = culture;
				CultureInfo.DefaultThreadCurrentUICulture = culture;
			}
			else
			{
				localSetting.Values["Language"] = "en-Us";

				var culture = new CultureInfo("en-Us");
				Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
				CultureInfo.DefaultThreadCurrentCulture = culture;
				CultureInfo.DefaultThreadCurrentUICulture = culture;

			}
		}
	}
}
