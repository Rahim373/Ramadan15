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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ramadan2015
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DetailPage : Page
	{
		private RozaModel roza;
		DispatcherTimer timer;
		public DetailPage()
		{
			this.InitializeComponent();
			HardwareButtons.BackPressed += HardwareButtons_BackPressed;
		
		}

		void setRemainingtime()
		{
			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		void timer_Tick(object sender, object e)
		{
			var tti = roza.Iftar- DateTime.Now;
			var tts = DateTime.Now - DateTime.Now.AddMinutes(25);
			var line = tts.Hours.ToString() + " Hours " + tts.Minutes.ToString() + " Minutes " + tts.Seconds.ToString();
			if (tts.Minutes <= 0) {
				line = "Already time over";
			}
			
			timetosehrir.Text = line;
			line = tti.Hours.ToString() + " Hours " + tti.Minutes.ToString() + " Minutes ";
			timetoifter.Text = line;
		}

		void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
		{
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null && rootFrame.CanGoBack)
			{
				timer.Stop();
				rootFrame.GoBack();
				e.Handled = true;
			}

		}


		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			roza = e.Parameter as RozaModel;
			Serial.Text = roza.Serial.ToString();
			Date.Text = string.Format("{0:D}", roza.Date);
			Iftar.Text = string.Format("{0:t}", roza.Iftar);
			Sehri.Text = string.Format("{0:t}", roza.Sahri);
			setRemainingtime();
		}
	}
}
