using Ramadan2015.Model;
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
			LoadLanguageCheckBox();
		}

		private void LoadLanguageCheckBox()
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
		
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

		}

		private void LocationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var location = (LocationNameAndTime)LocationList.SelectedItem ;
			localSetting.Values["LocationID"] = location.Id.ToString();
			localSetting.Values["Minute"] = location.minutes.ToString();
		}
	}
}
