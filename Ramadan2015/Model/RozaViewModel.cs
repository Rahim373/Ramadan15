using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramadan2015.Model
{
	public class RozaViewModel
	{
		public int Serial { get; set; }
		public DateTime Date { get; set; }
		public DateTime Sahri { get; set; }
		public DateTime Fazr { get; set; }
		public DateTime Johr { get; set; }
		public DateTime Asr { get; set; }
		public DateTime Iftar { get; set; }
		public DateTime Isha { get; set; }
		public string Colour { get; set; }
	}
}
