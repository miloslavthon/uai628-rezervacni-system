using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Poskytovatele
{
	public class Poskytovatel : DomainObject
	{
		public Poskytovatel()
		{
			this.TypRezervace = new TypRezervace();
		}

		public Poskytovatel(String nazev)
			: this()
		{
			Validate.NotNullOrEmpty(nazev, "Pro vytvoření poskytovatele musí být zadán jeho název.");

			this.Nazev = nazev;
		}

		public virtual String Nazev { get; set; }
		public virtual String Login { get; set; }
		public virtual int MaximalniPocetZverejnenychUdalosti { get; set; }
		public virtual int MaximalniPocetRezervaciJednohoKlienta { get; set; }
		public virtual TypRezervace TypRezervace { get; set; }
	}
}
