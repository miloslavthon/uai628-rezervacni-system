using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.TerminyRezervaci
{
	public interface ITerminRezervaceRepository
	{
		TerminRezervace VratTerminRezervaceDleDataProUpravy(int idTerminuUdalosti, DateTime datum);
		void Uloz(TerminRezervace terminRezervace);
	}
}
