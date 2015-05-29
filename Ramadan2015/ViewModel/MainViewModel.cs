using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Ramadan2015.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Devices;
using Windows.Phone.Devices.Notification;

namespace Ramadan2015.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;

		public MainViewModel()
		{
			LoadingData();

			if (localSetting.Values["Language"].ToString() == "bn-Bd") LoadLocationBd();
			else LoadLocation();

			getLocation();
		}


		public void getLocation() {
			getSelectedLocation = new LocationNameAndTime();
			int id = Convert.ToInt16(localSetting.Values["LocationID"]);
			foreach (var i in LocationAndTime){
				if (i.Id == id) {
					getSelectedLocation = i;
				}
			}
		}

		public const string getSelectedLocationPropertyName = "getSelectedLocation";

		private LocationNameAndTime _getLocation = null;

		public LocationNameAndTime getSelectedLocation
		{
			get
			{
				return _getLocation;
			}

			set
			{
				if (_getLocation == value)
				{
					return;
				}

				_getLocation = value;
				RaisePropertyChanged(getSelectedLocationPropertyName);
			}
		}

		#region Location

		private void LoadLocation()
		{
			LocationAndTime = new ObservableCollection<LocationNameAndTime>();
			LocationAndTime.Clear();

			LocationAndTime.Add(new LocationNameAndTime() { Id = 1, Name = "Dhaka", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 2, Name = "Chittagong", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 3, Name = "Sylhet", minutes = -1 });
		}

		private void LoadLocationBd()
		{
			LocationAndTime = new ObservableCollection<LocationNameAndTime>();
			LocationAndTime.Clear();

			LocationAndTime.Add(new LocationNameAndTime() { Id = 1, Name = "ঢাকা", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 2, Name = "চট্টগ্রাম", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 3, Name = "সিলেট", minutes = -1 });
		}

		public const string LocationAndTimePropertyName = "LocationAndTime";

		private ObservableCollection<LocationNameAndTime> _LocationAndTime = null;

		public ObservableCollection<LocationNameAndTime> LocationAndTime
		{
			get
			{
				return _LocationAndTime;
			}

			set
			{
				if (_LocationAndTime == value)
				{
					return;
				}

				_LocationAndTime = value;
				RaisePropertyChanged(LocationAndTimePropertyName);
			}
		}


		#endregion

		#region LoadMainListData
		async void LoadingData()
		{
			LoadData = new ObservableCollection<RozaViewModel>();
			try
			{
				StorageFolder SFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
				StorageFile SFile = await SFolder.GetFileAsync("RamdanData.txt");
				var Content = await FileIO.ReadTextAsync(SFile);
				var data = JsonConvert.DeserializeObject<List<RozaModel>>(Content);
				foreach (RozaModel i in data)
				{
					string c;
					if (i.Serial <= 10 && i.Serial > 0) c = "#750C0032";
					else if (i.Serial < 20 && i.Serial > 10) c = "#7500005E";
					else c = "#753100A0";
					LoadData.Add(new RozaViewModel()
					{
						Serial = (int)i.Serial,
						Date = (DateTime)i.Date,
						Fazr = (DateTime)i.Fazr,
						Johr = (DateTime)i.Johr,
						Asr = (DateTime)i.Asr,
						Iftar = (DateTime)i.Iftar,
						Isha = (DateTime)i.Isha,
						Colour = c
					});
				}
			}
			catch (Exception ex)
			{
				//doss(ex.ToString());
			}
		}

		async void doss(string s)
		{
			MessageDialog msg = new MessageDialog(s, "Error!");
			VibrationDevice vibrate = VibrationDevice.GetDefault();
			vibrate.Vibrate(TimeSpan.FromSeconds(0.2));
			await msg.ShowAsync();
		}

		public const string LoadDataPropertyName = "LoadData";
		private ObservableCollection<RozaViewModel> _Roza = null;
		public ObservableCollection<RozaViewModel> LoadData
		{
			get
			{
				return _Roza;
			}

			set
			{
				if (_Roza == value)
				{
					return;
				}

				_Roza = value;
				RaisePropertyChanged(LoadDataPropertyName);
			}
		} 
		#endregion
	}
}