using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Terminy
{
	public interface ITerminUdalostiRepository
	{
		TerminUdalosti Vrat(int id);
		void Uloz(TerminUdalosti terminUdalosti);
		IList<TerminUdalosti> VratTerminyDleUdalosti(int idUdalosti);
		IList<TerminUdalosti> VratAktualnePlatneTerminyDleUdalosti(int idUdalosti);
	}
}
