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
		DispatcherTimer timer;
		int timeCount = 0;

		public SplashPage()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Required;
			this.Loaded += MainPage_Loaded;
		}

		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += timer_Tick;
			timer.Start();


		}

		void timer_Tick(object sender, object e)
		{
			if (timeCount >= 1)
			{
				timer.Stop();
				WriteFile();
			}
			else
			{
				timeCount++;
			}
		}

		async void WriteFile()
		{
			try
			{
				List<RozaModel> DataToPass = null;
				using (HttpClient client = new HttpClient())
				{
					_state.Text = "Downloading updated file";
					Uri link = new Uri(@"http://rahim373-001-site1.myasp.net/api/values", UriKind.Absolute);
					var Content = await client.GetStringAsync(link);
					DataToPass = JsonConvert.DeserializeObject<List<RozaModel>>(Content);
					_state.Text = "Saving update";
					StorageFolder SFolder = ApplicationData.Current.LocalFolder;
					StorageFile SFile = await SFolder.CreateFileAsync("RamdanData.txt", CreationCollisionOption.ReplaceExisting);
					await FileIO.WriteTextAsync(SFile, Content);
					_state.Text = "Successfully saved";
				}
				var success = true;
				if (success)
				{
					_state.Text = "Loading data";
					Frame.Navigate(typeof(HomePage), DataToPass);
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
				_state.Text = "Checking current data file location";
				StorageFolder SFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
				StorageFile SFile = await SFolder.GetFileAsync("RamdanData.txt");
				var success = true;
				_state.Text = "Data file found";
				var Content = await FileIO.ReadTextAsync(SFile);
				var data = JsonConvert.DeserializeObject<List<RozaModel>>(Content);
				if (success)
				{
					_state.Text = "Loading data";
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
