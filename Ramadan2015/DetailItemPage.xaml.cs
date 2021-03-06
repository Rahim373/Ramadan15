﻿using Ramadan2015.Model;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Ramadan2015
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DetailItemPage : Page
	{
		DispatcherTimer timer;
		RozaViewModel item;
		public DetailItemPage()
		{
			this.InitializeComponent();

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += timer_Tick;
			timer.Start();

			Frame mainFrame = Window.Current.Content as Frame;
			mainFrame.ContentTransitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom } };
		}

		void timer_Tick(object sender, object e)
		{
			var a = (item.Sehri - DateTime.Now);
			var b = (item.Iftar - DateTime.Now);

			if (a.Seconds < 0)
			{
				_str.Text = "সময় শেষ";
			}
			else {
				_str.Text = a.Days + " : " + a.Hours + " : " + a.Minutes + " : " + a.Seconds;
			}


			if (b.Seconds < 0)
			{
				_itr.Text = "সময় শেষ";
			}
			else
			{
				_itr.Text = b.Days + " : " + b.Hours + " : " + b.Minutes + " : " + b.Seconds;
			}	
		}


		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			item = e.Parameter as RozaViewModel;
			_date.Text = string.Format("{0:dd MMMM yyyy}", item.Date);

			_day.Text = item.Serial.ToString();
			_sehri.Text = string.Format("{0:t}", item.Sehri);
			_ifter.Text = string.Format("{0:t}", item.Iftar);
			
		}
	}
}
