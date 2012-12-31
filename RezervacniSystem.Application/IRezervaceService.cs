using RezervacniSystem.Domain.Model.Terminy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application
{
	public interface IRezervaceService
	{
		List<Tuple<DateTime, TerminUdalosti>> VyhledatVolneTerminy(int idUdalosti, DateTime datumOd, DateTime datumDo);
		void RezervovatTermin(int idTerminuUdalosti, DateTime datum);
	}
}
