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
		}

		public Poskytovatel(String nazev)
		{
			Validate.NotNullOrEmpty(nazev, "Pro vytvoření poskytovatele musí být zadán jeho název.");

			this.Nazev = nazev;
		}

		public virtual String Nazev { get; set; }
	}
}
