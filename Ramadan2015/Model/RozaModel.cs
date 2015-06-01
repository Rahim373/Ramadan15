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
		public DateTime Sehri { get; set; }
		public DateTime Iftar { get; set; }
		public string Type { get; set; }
	}
}
