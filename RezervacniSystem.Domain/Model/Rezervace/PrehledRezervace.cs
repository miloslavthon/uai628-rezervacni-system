using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Rezervace
{
	public class PrehledRezervace
	{
		public PrehledRezervace(int id, DateTime datum, TimeSpan dobaTrvani, String udalost, String poskytovatel, TimeSpan uzaverkaRezervaci)
		{
			this.Id = id;
			this.Datum = datum;
			this.DobaTrvani = dobaTrvani;
			this.Udalost = udalost;
			this.Poskytovatel = poskytovatel;
			this.UzaverkaRezervaci = uzaverkaRezervaci;
		}

		public int Id { get; private set; }
		public DateTime Datum { get; private set; }
		public TimeSpan DobaTrvani { get; private set; }
		public String Udalost { get; private set; }
		public String Poskytovatel { get; private set; }
		public TimeSpan UzaverkaRezervaci { get; private set; }
	}
}
