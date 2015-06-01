using System;
using System.Collections.Generic;
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
using Windows.Web.Http;
using Newtonsoft.Json;
using Ramadan2015.Model;
using Windows.Storage;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Ramadan2015
{
	public sealed partial class SplashPage : Page
	{
		public SplashPage()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Required;
			this.Loaded += MainPage_Loaded;
		}

		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			WriteFile();
		}

		async void WriteFile()
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					Uri link = new Uri(@"http://rahim373-001-site1.myasp.net/api/values", UriKind.Absolute);
					var Content = await client.GetStringAsync(link);
					var data = JsonConvert.DeserializeObject<List<RozaModel>>(Content);
					StorageFolder SFolder = ApplicationData.Current.LocalFolder;
					StorageFile SFile = await SFolder.CreateFileAsync("RamdanData.txt", CreationCollisionOption.ReplaceExisting);
					await FileIO.WriteTextAsync(SFile, Content);
				}
				var success = true;
				if (success)
				{
					Frame.Navigate(typeof(HomePage));
				}
			}
			catch (Exception exception)
			{
				CheckDataFile();
			}
		}

		async void CheckDataFile()
		{
			try
			{
				StorageFolder SFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
				StorageFile SFile = await SFolder.GetFileAsync("RamdanData.txt");
				var success = true;
				var Content = await FileIO.ReadTextAsync(SFile);
				var data = JsonConvert.DeserializeObject<List<RozaModel>>(Content);
				if (success)
				{
					Frame.Navigate(typeof(HomePage), data);
				}
			}
			catch (Exception ex)
			{
				Dialog("Please connect to the internet. Appdata downloading failed.");
			}
		}

		async void Dialog(string text)
		{
			MessageDialog msg = new MessageDialog(text, "Error");
			await msg.ShowAsync();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

		}
	}
}
