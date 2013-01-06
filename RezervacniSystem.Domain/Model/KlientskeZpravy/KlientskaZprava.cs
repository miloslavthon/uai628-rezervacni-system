using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.KlientskeZpravy
{
	public class KlientskaZprava : DomainObject
	{
		public KlientskaZprava()
		{
		}

		public KlientskaZprava(Klient klient, String text)
		{
			Validate.NotNull(klient, "Musí být určen platný klient.");
			Validate.NotNullOrEmpty(text, "Musí být určen text zprávy.");

			this.Klient = klient;
			this.Datum = DateTime.Now;
			this.Text = text;
		}

		public virtual Klient Klient { get; set; }
		public virtual DateTime Datum { get; set; }
		public virtual String Text { get; set; }
	}
}
