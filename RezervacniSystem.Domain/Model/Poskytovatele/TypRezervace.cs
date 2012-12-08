using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Poskytovatele
{
	public class TypRezervace
	{
		public virtual bool UdalostiProViceOsob { get; set; }
		public virtual bool UdalostiMajiOpakovanyTermin { get; set; }
	}
}
