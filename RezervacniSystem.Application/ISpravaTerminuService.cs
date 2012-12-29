using RezervacniSystem.Domain.Model.Terminy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application
{
	public interface ISpravaTerminuService
	{
		TerminUdalosti ZverejnitJednorazovyTermin(int idUdalosti, DateTime datum, TimeSpan cas, TimeSpan dobaTrvani, TimeSpan uzaverkaRezervaci, String poznamka);
		TerminUdalosti ZverejnitOpakovanyTermin(int idUdalosti, DenVTydnu den, TimeSpan cas, DateTime platnyDo, TimeSpan dobaTrvani, TimeSpan uzaverkaRezervaci, String poznamka);
	}
}
