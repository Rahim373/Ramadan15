using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramadan2015.Model
{
	public class RozaModel
	{
		public int Serial { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Sahri { get; set; }
		public TimeSpan Fazr { get; set; }
		public TimeSpan Johr { get; set; }
		public TimeSpan Asr { get; set; }
		public TimeSpan Iftar { get; set; }
		public TimeSpan Isha { get; set; }
	}
}
