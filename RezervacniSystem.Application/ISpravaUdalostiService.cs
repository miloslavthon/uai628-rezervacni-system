using RezervacniSystem.Domain.Model.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application
{
	public interface ISpravaUdalostiService
	{
		Udalost ZalozitNovouUdalost(int idPoskytovatele, String nazev);
		void ZmenitUdalost(int idUdalosti, String nazev, int maximalniPocetUcastniku, bool zverejneno, String popis);
	}
}
