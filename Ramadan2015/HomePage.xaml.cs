using Ramadan2015.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.System;
using Windows.ApplicationModel.Store;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ramadan2015
{
	
	public sealed partial class HomePage : Page
	{
		List<RozaModel> data;
		Frame mainFrame = Window.Current.Content as Frame;
		Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;

		public HomePage()
		{
			this.InitializeComponent();
			this.Loaded += HomePage_Loaded;

			mainFrame.ContentTransitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom } };
		}

		void HomePage_Loaded(object sender, RoutedEventArgs e)
		{

			Date.Text = string.Format("{0:ddd, d MMMM, yy}", DateTime.Today);
			_Location.Text = localSetting.Values["Name"].ToString();


			var sub = (DateTime)DateTime.Today - data[0].Date;
			var cheackDate = sub.Days;
			if (cheackDate < 0)
			{
				Day.Text = cheackDate.ToString();
				if (localSetting.Values["Language"].ToString() != "bn-BD")
				{
					Day.Text = Math.Abs(cheackDate) + " Days remining"; 
					Sehri.Text = "Wait for Ramadan";
					Iftar.Text = "Wait for Ramadan";
				}
				else
				{
					Day.Text = Math.Abs(cheackDate) + " দিন বাকি";
					Sehri.Text = "অপেক্ষা করুন ";
					Iftar.Text = "অপেক্ষা করুন ";
				}
			}
			else if(cheackDate < data.Count())
			{
				foreach (var i in data)
				{
					if (DateTime.Today.Day == i.Date.Day)
					{
						int min = Convert.ToInt16(localSetting.Values["Minute"].ToString());
						Day.Text = i.Serial.ToString();
						Sehri.Text = string.Format("{0: h:mm tt}", i.Sehri + new TimeSpan(0, min, 0));
						Iftar.Text = string.Format("{0: h:mm tt}", i.Iftar + new TimeSpan(0, min, 0));
						break;
					}
				}
			}
			else
			{
				if (localSetting.Values["Language"].ToString() != "bn-BD")
				{
					Day.Text = "End of Ramadan";
					Sehri.Text = "End of Ramadan";
					Iftar.Text = "End of Ramadan";
				}
				else
				{
					Day.Text = "রমজান শেষ";
					Sehri.Text = "রমজান শেষ";
					Iftar.Text = "রমজান শেষ";
				}
				
			}
		}



		private void _calender_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(Calender));
		}

		private void _doa_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Duah));
		}

		private void _others_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Rules));
		}

		private void _setting_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingPage));
		}

		private void about_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(AboutPage));
		}

		private async void rate_Click(object sender, RoutedEventArgs e)
		{
			await Launcher.LaunchUriAsync(new Uri(@"ms-windows-store:reviewapp?appid="+ CurrentApp.AppId));
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			mainFrame.BackStack.Clear();

			data = e.Parameter as List<RozaModel>;
		}
	}

	public sealed class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Retrieve the format string and use it to format the value.
            string formatString = parameter as string;
            if (!string.IsNullOrEmpty(formatString))
            {
                return string.Format("{0:D}", value);
            }
			value = string.Format("{0:D}", value);
            return value.ToString();
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, 
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

	public sealed class TimeFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			// Retrieve the format string and use it to format the value.
			string formatString = parameter as string;
			if (!string.IsNullOrEmpty(formatString))
			{
				return string.Format("{0: h:mm tt}", value);
			}
			value = string.Format("{0: h:mm tt}", value);
			return value.ToString();
		}

		// No need to implement converting back on a one-way binding 
		public object ConvertBack(object value, Type targetType,
			object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}



}
