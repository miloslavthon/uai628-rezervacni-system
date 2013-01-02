using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Rezervace
{
	public class RezervaceTerminu : DomainObject
	{
		public RezervaceTerminu()
		{
		}

		public RezervaceTerminu(Klient klient, TerminRezervace termin)
		{
			Validate.NotNull(klient, "Musí být určen platný klient.");
			Validate.NotNull(termin, "Musí být určen platný termín rezervace.");

			this.Klient = klient;
			this.Termin = termin;
			this.Poskytovatel = termin.TerminUdalosti.Udalost.Poskytovatel;
		}

		public virtual Klient Klient { get; set; }
		public virtual TerminRezervace Termin { get; set; }
		public virtual Poskytovatel Poskytovatel { get; set; }
	}
}
