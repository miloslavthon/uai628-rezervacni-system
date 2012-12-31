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
		IList<TerminUdalosti> VratPlatneJednorazoveTerminyDleUdalosti(int idUdalosti, DateTime datumOd, DateTime datumDo);
		IList<TerminUdalosti> VratPlatneOpakovaneTerminyDleUdalosti(int idUdalosti, DateTime datum);
		void Odstran(int id);
		void OdstranVsechnyTerminyUdalosti(int idUdalosti);
		void OdstranVsechnyTerminyUdalostiProPoskytovatele(int idPoskytovatele);
	}
}
