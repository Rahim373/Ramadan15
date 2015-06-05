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
				cheackBox.IsChecked = false;
			}
			else
			{
				cheackBox.IsChecked = true;
			}
		}


		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			var location = (LocationNameAndTime)LocationList.SelectedItem;
			localSetting.Values["LocationID"] = location.Id.ToString();
			localSetting.Values["Name"] = location.Name.ToString();
			localSetting.Values["Minute"] = location.minutes.ToString();


			if (this.Frame.CanGoBack)
			{
				this.Frame.GoBack();
			}

		}

		private void cheackBox_Checked(object sender, RoutedEventArgs e)
		{
			localSetting.Values["Language"] = "bn-Bd";
			var culture = new CultureInfo("bn-Bd");
			Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;
		}

		private void cheackBox_Unchecked(object sender, RoutedEventArgs e)
		{
			localSetting.Values["Language"] = "en-Us";
			var culture = new CultureInfo("en-Us");
			Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			if (this.Frame.CanGoBack) {
				this.Frame.GoBack();
			}
		}


	}
}
