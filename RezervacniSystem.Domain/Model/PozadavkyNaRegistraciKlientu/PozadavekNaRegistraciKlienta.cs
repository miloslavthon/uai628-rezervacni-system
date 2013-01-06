using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.PozadavkyNaRegistraciKlientu
{
	public class PozadavekNaRegistraciKlienta : DomainObject
	{
		public PozadavekNaRegistraciKlienta()
		{
		}

		public PozadavekNaRegistraciKlienta(Klient klient, Poskytovatel poskytovatel)
		{
			Validate.NotNull(klient, "Musí být určen platný klient.");
			Validate.NotNull(poskytovatel, "Musí být určen platný poskytovatel.");

			this.Klient = klient;
			this.Poskytovatel = poskytovatel;
			this.Datum = DateTime.Now;
		}

		public virtual Klient Klient { get; set; }
		public virtual Poskytovatel Poskytovatel { get; set; }
		public virtual DateTime Datum { get; set; }
	}
}
