using Ramadan2015.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ramadan2015
{

	public sealed partial class Calender : Page
	{

		public Calender()
		{
			this.InitializeComponent();
			Frame mainFrame = Window.Current.Content as Frame;
			mainFrame.ContentTransitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom } };
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

		}


		private void MainList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			RozaViewModel item = (RozaViewModel)MainList.SelectedItem;
			Frame.Navigate(typeof(DetailItemPage), item);
		}
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
		//value = string.Format("{0:D}", value);

		return value.ToString();
	}

	// No need to implement converting back on a one-way binding 
	public object ConvertBack(object value, Type targetType,  object parameter, string language)
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
