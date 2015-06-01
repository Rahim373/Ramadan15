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

			ChangeListLanguage();

			getLocation();
			LoadLocationClearly = new RelayCommand(getLocation);

			ChangeLanguageUpdateforList = new RelayCommand(ChangeListLanguage);

			UpdateTimesList = new RelayCommand(LoadingData);
		}

		
		

		#region UpdateTimeRelay
		public const string UpdateTimesListPropertyName = "UpdateTimesList";

		private RelayCommand _UpdateTimes = null;
		public RelayCommand UpdateTimesList
		{
			get
			{
				return _UpdateTimes;
			}

			set
			{
				if (_UpdateTimes == value)
				{
					return;
				}

				_UpdateTimes = value;
				RaisePropertyChanged(UpdateTimesListPropertyName);
			}
		} 
		#endregion

		#region LanguageChangeUpdate

		public const string ChangeLanguageUpdateforListPropertyName = "ChangeLanguageUpdateforList";
		private RelayCommand _ChangeLanguage = null;
		public RelayCommand ChangeLanguageUpdateforList
		{
			get
			{
				return _ChangeLanguage;
			}

			set
			{
				if (_ChangeLanguage == value)
				{
					return;
				}

				_ChangeLanguage = value;
				RaisePropertyChanged(ChangeLanguageUpdateforListPropertyName);
			}
		} 
		#endregion

		#region LoadLocationUpdate

		public const string LoadLocationClearlyPropertyName = "LoadLocationClearly";
		private RelayCommand _LoadLocationClearly = null;
		public RelayCommand LoadLocationClearly
		{
			get
			{
				return _LoadLocationClearly;
			}

			set
			{
				if (_LoadLocationClearly == value)
				{
					return;
				}

				_LoadLocationClearly = value;
				RaisePropertyChanged(LoadLocationClearlyPropertyName);
			}
		}


		#endregion

		#region GetSelected
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
		#endregion

		#region Location

		private void ChangeListLanguage()
		{
			if (localSetting.Values["Language"].ToString() == "bn-Bd") LoadLocationBd();
			else LoadLocation();
		}

		private void LoadLocation()
		{
			LocationAndTime = new ObservableCollection<LocationNameAndTime>();
			LocationAndTime.Clear();

			LocationAndTime.Add(new LocationNameAndTime() { Id = 1, Name = "Bagerhat", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 2, Name = "Bandarban", minutes = -8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 3, Name = "Barguna", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 4, Name = "Barisal", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 5, Name = "Bhola", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 6, Name = "Bogra", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 7, Name = "Brahmanbaria", minutes = -3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 8, Name = "Chandpur", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 9, Name = "Chapainawabganj", minutes = 9 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 10, Name = "Chittagong", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 11, Name = "Chuadanga", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 12, Name = "Comilla", minutes = -4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 13, Name = "Cox's Bazar", minutes = -7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 14, Name = "Dhaka", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 15, Name = "Dinajpur", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 16, Name = "Faridpur", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 17, Name = "Feni", minutes = -5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 18, Name = "Gaibandha", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 19, Name = "Gazipur", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 20, Name = "Gopalganj", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 21, Name = "Habiganj", minutes = -5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 22, Name = "Jamalpur", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 23, Name = "Jessore", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 24, Name = "Jhalokati", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 25, Name = "Jhenaidah", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 26, Name = "Joypurhat", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 27, Name = "Khagrachhari", minutes = -7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 28, Name = "Khulna", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 29, Name = "Kishoreganj", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 30, Name = "Kurigram", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 31, Name = "Kushtia", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 32, Name = "Lakshmipur", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 33, Name = "Lalmonirhat", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 34, Name = "Madaripur", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 35, Name = "Magura", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 36, Name = "Manikganj", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 37, Name = "Meherpur", minutes = 8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 38, Name = "Moulvibazar", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 39, Name = "Munshiganj", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 40, Name = "Mymensingh", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 41, Name = "Naogaon", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 42, Name = "Narail", minutes = 4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 43, Name = "Narayanganj", minutes = -1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 44, Name = "Narsingdi", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 45, Name = "Natore", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 46, Name = "Netrakona", minutes = -2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 47, Name = "Nilphamari", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 48, Name = "Noakhali", minutes = -3});
			LocationAndTime.Add(new LocationNameAndTime() { Id = 49, Name = "Pabna", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 50, Name = "Panchagarh", minutes = 8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 51, Name = "Patuakhali", minutes = 1 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 52, Name = "Pirojpur", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 53, Name = "Rajbari", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 54, Name = "Rajshahi", minutes = 7 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 55, Name = "Rangamati", minutes = -8 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 56, Name = "Rangpur", minutes = 5 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 57, Name = "Satkhira", minutes = 6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 58, Name = "Shariatpur", minutes = 0 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 59, Name = "Sherpur", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 60, Name = "Sirajganj", minutes = 3 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 61, Name = "Sunamganj", minutes = -4 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 62, Name = "Sylhet", minutes = -6 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 63, Name = "Tangail", minutes = 2 });
			LocationAndTime.Add(new LocationNameAndTime() { Id = 64, Name = "Thakurgaon", minutes = 8 });
		}

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
						Sahri = (DateTime)i.Sahri + new TimeSpan(0, min, 0),
						Date = (DateTime)i.Date,
						Fazr = (DateTime)i.Fazr,
						Johr = (DateTime)i.Johr,
						Asr = (DateTime)i.Asr,
						Iftar = (DateTime)i.Iftar + new TimeSpan(0, min, 0),
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