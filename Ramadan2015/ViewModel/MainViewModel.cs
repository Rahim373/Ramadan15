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


		public MainViewModel()
		{
			LoadingData();
		}

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
					if (i.Serial <= 10 && i.Serial > 0) c = "#0C0032";
					else if (i.Serial < 20 && i.Serial > 10) c = "#00005E";
					else c = "#3100A0";
					LoadData.Add(new RozaViewModel() {
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
				doss(ex.ToString());
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
	}
}