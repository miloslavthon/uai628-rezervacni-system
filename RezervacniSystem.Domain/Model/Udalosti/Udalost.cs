using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Udalosti
{
	public class Udalost : DomainObject
	{
		public Udalost()
		{
		}

		public Udalost(Poskytovatel poskytovatel, String nazev)
		{
			Validate.NotNull(poskytovatel, "Pro vytvoření události musí být určen poskytovatel.");
			Validate.NotNullOrEmpty(nazev, "Musí být určen název události.");

			this.Poskytovatel = poskytovatel;
			this.Nazev = nazev;
		}

		public virtual Poskytovatel Poskytovatel { get; set; }
		public virtual String Nazev { get; set; }
		/// <summary>
		/// Určuje počet možných rezervací pro jednotlivý termín dané událost. 0 - neomezeno.
		/// </summary>
		public virtual int MaximalniPocetUcastniku { get; set; }
		public virtual bool OpakovanyTermin { get; set; }
		public virtual bool Zverejneno { get; set; }
		public virtual String Popis { get; set; }
	}
}
