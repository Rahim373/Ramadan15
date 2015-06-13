using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
			LoadLocationBd();
			UpdateTime = new RelayCommand(LoadingData);
		}

		#region UpdateTime

		public const string UpdateTimePropertyName = "UpdateTime";

		private RelayCommand _UpdateTime = null;

		public RelayCommand UpdateTime
		{
			get
			{
				return _UpdateTime;
			}

			set
			{
				if (_UpdateTime == value)
				{
					return;
				}

				_UpdateTime = value;
				RaisePropertyChanged(UpdateTimePropertyName);
			}
		}

		#endregion

		#region Location


		private void LoadLocationBd()
		{
			LocationAndTime = new ObservableCollection<LocationNameAndTime>();
			LocationAndTime.Clear();

			LocationAndTime.Add(new LocationNameAndTime() { Id = 1, Name = "বাগেরহাট", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 2, Name = "বান্দরবান", minutes = -8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 3, Name = "বরগুনা", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 4, Name = "বরিশাল", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 5, Name = "ভোলা", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 6, Name = "বগুড়া", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 7, Name = "ব্রাহ্মণবাড়ীয়া", minutes = -3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 8, Name = "চাঁদপুর", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 9, Name = "চাঁপাইনবাবগঞ্জ", minutes = 9 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 10, Name = "চট্টগ্রাম", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 11, Name = "চুয়াডাঙ্গা", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 12, Name = "কুমিল্লা", minutes = -4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 13, Name = "কক্সবাজার", minutes = -7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 14, Name = "ঢাকা", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 15, Name = "দিনাজপুর", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 16, Name = "ফরিদপুর", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 17, Name = "ফেনী", minutes = -5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 18, Name = "গাইবান্ধা", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 19, Name = "গাজীপুর", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 20, Name = "গোপালগঞ্জ", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 21, Name = "হবিগঞ্জ", minutes = -5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 22, Name = "জামালপুর", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 23, Name = "যশোর", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 24, Name = "ঝালকাঠি", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 25, Name = "ঝিনাইদহ", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 26, Name = "জয়পুরহাট", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 27, Name = "খাগড়াছড়ি", minutes = -7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 28, Name = "খুলনা", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 29, Name = "কিশোরগঞ্জ", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 30, Name = "কুড়িগ্রাম", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 31, Name = "কুষ্টিয়া", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 32, Name = "লক্ষীপুর", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 33, Name = "লালমনিরহাট", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 34, Name = "মাদারীপুর", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 35, Name = "মাগুরা", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 36, Name = "মানিকগঞ্জ", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 37, Name = "মেহেরপুর", minutes = 8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 38, Name = "মৌলভীবাজার", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 39, Name = "মুন্সীগঞ্জ", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 40, Name = "ময়মনসিংহ", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 41, Name = "নওগাঁ", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 42, Name = "নড়াইল", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 43, Name = "নারায়ণগঞ্জ", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 44, Name = "নরসিংদী", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 45, Name = "নাটোর", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 46, Name = "নেত্রকোনা", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 47, Name = "নীলফামারী", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 48, Name = "নোয়াখালী", minutes = -3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 49, Name = "পাবনা", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 50, Name = "পঞ্চগড়", minutes = 8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 51, Name = "পটুয়াখালী", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 52, Name = "পিরোজপুর", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 53, Name = "রাজবাড়ী", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 54, Name = "রাজশাহী", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 55, Name = "রাঙ্গামাটি", minutes = -8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 56, Name = "রংপুর", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 57, Name = "সাতক্ষিরা", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 58, Name = "শরীয়তপুর", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 59, Name = "শেরপুর", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 60, Name = "সিরাজগঞ্জ", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 61, Name = "সুনামগঞ্জ", minutes = -4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 62, Name = "সিলেট", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 63, Name = "টাঙ্গাইল", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 64, Name = "ঠাকুরগাঁ", minutes = 8 });
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
					int min = Convert.ToInt16(localSetting.Values["Minute"].ToString());
					LoadData.Add(new RozaViewModel()
					{
						Serial = (int)i.Serial,
						Sehri = (DateTime)i.Sehri + new TimeSpan(0, min, 0),
						Date = (DateTime)i.Date,
						Iftar = (DateTime)i.Iftar + new TimeSpan(0, min, 0),
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